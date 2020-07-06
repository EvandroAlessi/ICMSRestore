using BLL;
using CrossCutting;
using DAO;
using System;
using System.Collections.Generic;
using System.IO;
using Uploader.Models;

namespace Uploader
{
    class Program
    {
        static void Main(string[] args)
        {
            AppSettings.ConnectionString = "Server = 127.0.0.1; Port = 5432; Database = icms_restore; User Id = postgres; Password = admin";

            try
            {
                //SendFiles();
                //DeleteAllNFe();

                foreach (var processo in new ProcessoService().GetAll().Result)
                {
                    var aux = new ItemFiltradoDAO().GetSumarry(processo).Result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }


        public static void DeleteAllNFe()
        {
            try
            {
                var nfeService = new NFeService();

                foreach (var nfe in nfeService.GetAllSimplify(1, 100000).Result)
                {
                    nfeService.Delete(nfe.ID);
                    Console.WriteLine(nfe.ID);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void SendFiles()
        {
            try
            {
                var fileInfos = new List<FileInfo>();
                string[] filePaths = Directory.GetFiles(@"C:\Users\evand\Downloads\ENTRADAS 04.2020 - MADEIREIRA BINACHINI");

                foreach (var filePath in filePaths)
                {
                    var fileInfo = new FileInfo(filePath);

                    fileInfos.Add(fileInfo);
                }

                var uploadRestClientModel = new UploadRestClientModel();
                var result = uploadRestClientModel.Upload(fileInfos).Result;

                var fileResults = result.Content.ReadAsAsync<List<FileUploadResult>>().Result;

                Console.WriteLine("Status: " + result.StatusCode);

                foreach (var fileResult in fileResults)
                {
                    Console.WriteLine("File name: " + fileResult.Name);
                    Console.WriteLine("File size: " + fileResult.Length);
                    Console.WriteLine("====================");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
