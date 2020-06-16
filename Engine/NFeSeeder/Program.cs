using BLL;
using CrossCutting;
using CrossCutting.Serializable;
using CrossCutting.SerializationModels;
using Microsoft.EntityFrameworkCore.Query.Internal;
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


        public static void RemoveCompletedFolder(int processoID, string subDir)
        {
            try
            {
                processDirs[processoID].Remove(subDir);

                if (Directory.GetFiles(subDir).Count() == 0)
                {
                    Directory.Delete(subDir);

                    if (processDirs[processoID].Values.Count == 0)
                    {
                        var processPath = dirPaths.FirstOrDefault(x => Path.GetFileName(x).Equals(processoID.ToString()));

                        processDirs.Remove(processoID);
                        dirPaths.Remove(processPath);
                    }
                }
            }
            catch (Exception ex)
            {
                Log(func: $"Program.{ MethodBase.GetCurrentMethod().Name }", ex);
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
                                        })
                                        .ContinueWith(t => {
                                            RemoveCompletedFolder(process.Key, subDir.Key);
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
                bool entrada = subDir.Split("\\").Last().StartsWith("$@-");

                using (var serializable = new NFeSerialization())
                {
                    foreach (var filePath in filePaths)
                    {
                        int triesCount = 0;
                    start:
                        var nfe = new NFe();

                        try
                        {
                            try
                            {
                                nfe = serializable.GetObjectFromFile<NFe>(filePath);
                            }
                            catch
                            {
                                nfe = serializable.GetObjectFromFile<NFeProc>(filePath)?.NotaFiscalEletronica;
                            }

                            if (nfe?.InformacoesNFe != null)
                            {
                                var inserted = nfeService.Insert(nfe, processoID, entrada);

                                if (inserted)
                                {
                                    File.Delete(filePath);
                                }
                                else if (triesCount < 2)
                                {
                                    triesCount++;

                                    Thread.Sleep(100);

                                    goto start;
                                }
                                else
                                {
                                    ControlErrorFiles(processoID, subDir, filePath);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            if (nfe?.InformacoesNFe?.Identificacao?.cNF != null && nfe?.InformacoesNFe?.Identificacao?.nNF != null)
                            {
                                var exists = nfeService.Exists(nfe.InformacoesNFe.Identificacao.cNF, nfe.InformacoesNFe.Identificacao.nNF, processoID).Result;

                                if (exists)
                                {
                                    var loggingRepeated = new
                                    {
                                        ProcessoID = processoID,
                                        cNF = nfe.InformacoesNFe.Identificacao.cNF,
                                        nNF = nfe.InformacoesNFe.Identificacao.nNF,
                                        XmlFile = Path.GetFileName(filePath)
                                    };

                                    Log(func: $"Program.{ MethodBase.GetCurrentMethod().Name }", ex, loggingRepeated, true);

                                    File.Delete(filePath);

                                    continue;
                                }
                            }

                            if (triesCount < 2)
                            {
                                triesCount++;

                                Thread.Sleep(100);

                                goto start;
                            }

                            ControlErrorFiles(processoID, subDir, filePath, ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log(func: $"Program.{ MethodBase.GetCurrentMethod().Name }", ex);
            }
        }

        public static void ControlErrorFiles(int processoID, string subDir, string filePath, Exception ex = null)
        {
            try
            {
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
            catch
            {
                throw ex;
            }
        }
    }
}
