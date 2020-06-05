using BLL;
using CrossCutting;
using CrossCutting.Serializable;
using CrossCutting.SerializationModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;

namespace NFeSeeder
{
    class Program
    {
        private static System.Timers.Timer timer;
        private static Dictionary<int, Dictionary<string, bool>> processDirs = new Dictionary<int, Dictionary<string, bool>>();
        private static List<string> dirPaths;

        static void Main(string[] args)
        {
            try
            {
                AppSettings.RootPath = @"C:\Users\evand\source\repos\ICMSRestore\Engine\API\wwwroot";
                AppSettings.ConnectionString = "Server=127.0.0.1;Port=5432;Database=icms_restore;User Id=postgres;Password=admin";
                dirPaths = Directory.GetDirectories(AppSettings.RootPath).ToList();

                foreach (var dirPath in dirPaths)
                {
                    string[] subDirs = Directory.GetDirectories(dirPath);

                    var aux = new Dictionary<string, bool>();

                    foreach (var subDir in subDirs)
                        aux.Add(subDir, false);

                    int processID = Convert.ToInt32(Path.GetFileName(dirPath));

                    processDirs.Add(processID, aux);
                }
                    

                timer = new System.Timers.Timer();
                timer.Interval = TimeSpan.FromSeconds(10).TotalMilliseconds;

                timer.Elapsed += new ElapsedEventHandler(OnElapsed);

                timer.AutoReset = true;

                timer.Enabled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

        private static void OnElapsed(object sender, ElapsedEventArgs e)
        {
            lock (dirPaths)
            {
                var currentProcessList = Directory.GetDirectories(AppSettings.RootPath).ToList();

                var newProcesses = currentProcessList.Except(dirPaths).ToList();

                if (newProcesses.Count() > 0)
                {
                    foreach (var dirPath in newProcesses)
                    {
                        string[] subDirs = Directory.GetDirectories(dirPath);

                        var aux = new Dictionary<string, bool>();

                        foreach (var subDir in subDirs)
                            aux.Add(subDir, false);

                        int processID = Convert.ToInt32(Path.GetFileName(dirPath));

                        processDirs.Add(processID, aux);
                        dirPaths.Add(dirPath);
                    }
                }

                foreach (var dirPath in dirPaths)
                {
                    var currentSubDirList = Directory.GetDirectories(dirPath).ToList();
                    int processID = Convert.ToInt32(Path.GetFileName(dirPath));

                    var newSubDirs = currentSubDirList.Except(processDirs[processID].Keys).ToList();

                    foreach (var subDir in newSubDirs)
                        processDirs[processID].Add(subDir, false);
                }
            }

            // Divide o processamento em pastas
            lock (processDirs)
            {
                foreach (var process in processDirs)
                {
                    try
                    {
                        foreach (var subDir in process.Value)
                        {
                            if (!subDir.Value)
                            {
                                processDirs[process.Key][subDir.Key] = true;

                                DoWork(process.Key, subDir.Key);
                            }
                        }
                    }
                    catch {  }
                }
            }
        }

        public static void DoWork(int processoID, string subDir)
        {
            try
            {
                var nfeService = new NFeService();
                var itemService = new ItemService();
                var processoService = new ProcessoService();

                string[] filePaths = Directory.GetFiles(subDir);
                bool allItemsAdded = false;

                using (var serializable = new NFeSerialization())
                {
                    foreach (var filePath in filePaths)
                    {
                        var nfe = new NFeProc();

                        try
                        {
                            nfe = serializable.GetObjectFromFile<NFeProc>(filePath);
                        }
                        catch (Exception ex)
                        {
                            //DO Something
                            continue;
                        }

                        if (nfe?.NotaFiscalEletronica?.InformacoesNFe != null)
                        {
                            try
                            {
                                if (nfeService.Insert(nfe, processoID))
                                {
                                    foreach (var detalhe in nfe.NotaFiscalEletronica.InformacoesNFe.Detalhe)
                                    {
                                        try
                                        {
                                            if (!itemService.Insert(detalhe, nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao.cNF))
                                            {
                                                allItemsAdded = false;
                                                break;
                                            }
                                            else
                                            {
                                                allItemsAdded = true;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            allItemsAdded = false;
                                            break;
                                        }
                                    }

                                    if (allItemsAdded)
                                    {
                                        File.Delete(filePath);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                var exists = nfeService.Exists(nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao.cNF).Result;

                                if (exists)
                                {
                                    File.Delete(filePath);
                                    continue;
                                }
                            }
                        }
                    }
                }

                if (Directory.GetFiles(subDir).Count() == 0)
                {
                    try
                    {
                        processDirs[processoID].Remove(subDir);
                        Directory.Delete(subDir);
                    }
                    catch { } 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
