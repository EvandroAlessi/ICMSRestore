using BLL;
using CrossCutting;
using CrossCutting.Serializable;
using CrossCutting.SerializationModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using static CrossCutting.LOG;

namespace NFeSeeder
{
    class Program
    {
        private static System.Timers.Timer timer;
        private static Dictionary<int, Dictionary<string, bool>> processDirs = new Dictionary<int, Dictionary<string, bool>>();
        private static List<string> dirPaths;

        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                IConfiguration config = new ConfigurationBuilder()
                     .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                     .AddEnvironmentVariables()
                     .AddCommandLine(args)
                     .Build();

                _ = new AppSettings(config);

                CultureConfiguration.ConfigureCulture();

                FindProcessFolders();

                OnElapsed(null, null);

                timer = new System.Timers.Timer();
                timer.Interval = TimeSpan.FromMinutes(AppSettings.TimerElapsed).TotalMilliseconds;

                timer.Elapsed += new ElapsedEventHandler(OnElapsed);

                timer.AutoReset = true;

                timer.Enabled = true;
            }
            catch (Exception ex)
            {
                Log(func: $"Program.{ MethodBase.GetCurrentMethod().Name }", ex);
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

        private static void OnElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                //Find new processes folders
                CheckNewFolders();

                // start working with all sub folders in parallel
                StartParallelWork();
            }
            catch (Exception ex)
            {
                Log(func: $"Program.{ MethodBase.GetCurrentMethod().Name }", ex);
            }
        }

        public static void FindProcessFolders()
        {
            try
            {
                dirPaths = Directory.GetDirectories(AppSettings.RootPath).ToList();

                lock (dirPaths)
                {
                    foreach (var dirPath in dirPaths)
                    {
                        var subFolders = FindProcessSubFolders(dirPath);

                        int processID = Convert.ToInt32(Path.GetFileName(dirPath));

                        processDirs.Add(processID, subFolders);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Dictionary<string, bool> FindProcessSubFolders(string dirPath)
        {
            try
            {
                string[] subDirs = Directory.GetDirectories(dirPath);

                var aux = new Dictionary<string, bool>();

                foreach (var subDir in subDirs)
                {
                    var insideFiles = Directory.GetFiles(subDir);

                    if (insideFiles.Count() > 0)
                    {
                        aux.Add(subDir, false);
                    }
                }

                return aux;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void CheckNewFolders()
        {
            try
            {
                lock (dirPaths)
                {
                    var currentProcessList = Directory.GetDirectories(AppSettings.RootPath).ToList();

                    var newProcesses = currentProcessList.Except(dirPaths).ToList();

                    foreach (var dirPath in newProcesses)
                    {
                        var subFolders = FindProcessSubFolders(dirPath);

                        int processID = Convert.ToInt32(Path.GetFileName(dirPath));

                        processDirs.Add(processID, subFolders);
                        dirPaths.Add(dirPath);
                    }

                    foreach (var dirPath in dirPaths)
                    {
                        var currentSubDirList = Directory.GetDirectories(dirPath).ToList();
                        int processID = Convert.ToInt32(Path.GetFileName(dirPath));

                        var newSubDirs = currentSubDirList.Except(processDirs[processID].Keys).ToList();

                        foreach (var subDir in newSubDirs)
                        {
                            var insideFiles = Directory.GetFiles(subDir);

                            if (insideFiles.Count() > 0)
                            {
                                processDirs[processID].Add(subDir, false);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void StartParallelWork()
        {
            try
            {
                lock (processDirs)
                {
                    foreach (var process in processDirs.ToList())
                    {
                        try
                        {
                            foreach (var subDir in process.Value.ToList())
                            {
                                if (!subDir.Value)
                                {
                                    processDirs[process.Key][subDir.Key] = true;

                                    Task.Run(() => {
                                        DoWork(process.Key, subDir.Key);
                                    });
                                }
                            }
                        }
                        catch (Exception ex) { }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DoWork(int processoID, string subDir)
        {
            try
            {
                var nfeService = new NFeService();
                string[] filePaths = Directory.GetFiles(subDir);

                using (var serializable = new NFeSerialization())
                {
                    foreach (var filePath in filePaths)
                    {
                        var nfe = new NFeProc();

                        try
                        {
                            nfe = serializable.GetObjectFromFile<NFeProc>(filePath);

                            if (nfe?.NotaFiscalEletronica?.InformacoesNFe != null)
                            {
                                nfeService.Insert(nfe, processoID);

                                File.Delete(filePath);
                            }
                        }
                        catch (Exception ex)
                        {
                            if (nfe?.NotaFiscalEletronica?.InformacoesNFe?.Identificacao?.cNF != null && nfe?.NotaFiscalEletronica?.InformacoesNFe?.Identificacao?.nNF != null)
                            {
                                var exists = nfeService.Exists(nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao.cNF, nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao.nNF).Result;

                                if (exists)
                                {
                                    File.Delete(filePath);

                                    continue;
                                }
                            }

                            var logging = new
                            {
                                ProcessoID = processoID,
                                ZipFolder = Path.GetFileName(subDir),
                                XmlFile = Path.GetFileName(filePath),
                                FullPath = filePath
                            };

                            Log(func: $"Program.{ MethodBase.GetCurrentMethod().Name }", ex, logging);

                            var errorpPath = Path.Combine(subDir, "Error");

                            PathControl.Create(errorpPath);

                            File.Move(filePath, Path.Combine(errorpPath, Path.GetFileName(filePath)));
                        }
                    }
                }

                if (Directory.GetFiles(subDir).Count() == 0)
                {
                    try
                    {
                        processDirs[processoID].Remove(subDir);

                        if (processDirs[processoID].Values.Count == 0)
                        {
                            var processPath = dirPaths.FirstOrDefault(x => Path.GetFileName(x).Equals(processoID.ToString()));

                            processDirs.Remove(processoID);
                            dirPaths.Remove(processPath);
                        }

                        Directory.Delete(subDir);
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                Log(func: $"Program.{ MethodBase.GetCurrentMethod().Name }", ex);
                throw ex;
            }
        }
    }
}
