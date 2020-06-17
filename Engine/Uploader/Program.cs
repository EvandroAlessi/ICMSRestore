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

            Console.ReadLine();
        }
    }
}
