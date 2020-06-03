using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Uploader
{
    public class UploadRestClientModel
    {
        private string BASE_URL = "https://localhost:44375/api/";

        public Task<HttpResponseMessage> Upload(List<FileInfo> fileInfos)
        {
            try
            {
                var httpClient = new HttpClient();
                var multipartFormDataContent = new MultipartFormDataContent();
                httpClient.BaseAddress = new Uri(BASE_URL);

                foreach (var fileInfo in fileInfos)
                {
                    var fileContent = new ByteArrayContent(File.ReadAllBytes(fileInfo.FullName));
                    multipartFormDataContent.Add(fileContent, "files", fileInfo.Name);
                }

                return httpClient.PostAsync("upload", multipartFormDataContent);
            }
            catch
            {
                return null;
            }
        }
    }
}
