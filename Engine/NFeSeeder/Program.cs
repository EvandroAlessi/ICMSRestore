using BLL;
using CrossCutting.Serializable;
using CrossCutting.SerializationModels;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace NFeSeeder
{
    class Program
    {
        private static System.Timers.Timer timer;

        static void Main(string[] args)
        {
            try
            {
                timer = new System.Timers.Timer();
                timer.Interval = TimeSpan.FromMinutes(5).TotalMilliseconds;

                timer.Elapsed += OnElapsed;

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
            DoWork();
        }

        public static void DoWork()
        {
            try
            {
                bool allItemsAdded = false;
                var nfeService = new NFeService();
                var itemService = new ItemService();
                var path = Path.Combine(@"C:\Users\evand\source\repos\ICMSRestore\Engine\API\wwwroot");
                string[] filePaths = Directory.GetFiles(path);

                using (var serializable = new NFeSerialization())
                {
                    foreach (var filePath in filePaths)
                    {
                        allItemsAdded = false;
                        var nfe = serializable.GetObjectFromFile<NFeProc>(filePath);

                        if (nfe?.NotaFiscalEletronica?.InformacoesNFe != null && nfeService.Insert(nfe))
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
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
