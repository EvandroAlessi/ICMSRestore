using API.Models;
using BLL;
using CrossCutting;
using Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly ILogger<UploadController> _logger;

        public UploadController(ILogger<UploadController> logger)
        {
            _logger = logger;
        }

        // POST: api/<UploadController>
        /// <summary>
        /// Send indicated files to our server
        /// </summary>
        /// <param name="files">Files types: XML e/ou ZIP</param>
        /// <param name="processoID">Process ID</param>
        /// <returns>An object list with name and file length</returns>
        [HttpPost("{processoID}")]
        [Consumes("multipart/form-data")]
        public IActionResult Post([FromForm]List<IFormFile> files, int processoID)
        {
            try
            {
                //var result = new List<FileUploadResult>();
                List<ProcessoUpload> processosUpload = new List<ProcessoUpload>();

                foreach (var file in files)
                {
                    var path = Path.Combine(AppSettings.RootPath, processoID.ToString());

                    if (Directory.Exists(path))
                    {
                        var processoService = new ProcessoService();

                        bool exists = processoService.Exists(processoID).Result;

                        if (!exists)
                        {
                            return BadRequest("Can't find indicated process, please, try again.");
                        }

                        if (!file.FileName.EndsWith(".zip"))
                        {
                            return BadRequest("The send file isn't a .ZIP file, please, try again.");

                            //if (file.FileName.EndsWith(".xml"))
                            //{
                            //    var filePath = Path.Combine(path, file.FileName);

                            //    if (!System.IO.File.Exists(filePath))
                            //    {
                            //        using (var fileStream = new FileStream(filePath, FileMode.Create))
                            //        {
                            //            await file.CopyToAsync(fileStream);

                            //            var responseFile = new FileUploadResult()
                            //            {
                            //                Name = file.FileName,
                            //                Length = file.Length
                            //            };

                            //            result.Add(responseFile);
                            //        }
                            //    }
                            //    else
                            //    {
                            //        return BadRequest("This file already exists in our server.");
                            //    }
                            //}
                        }

                        //Save the files in our server
                        using (var stream = file.OpenReadStream())
                        {
                            using (ZipArchive archive = new ZipArchive(stream))
                            {
                                var zipDir = Path.Combine(path, Path.ChangeExtension(file.FileName, null));

                                PathControl.Create(zipDir);

                                var entries = archive.Entries.Where(x => !string.IsNullOrWhiteSpace(x.Name));

                                // One By One Code
                                foreach (ZipArchiveEntry entry in entries)
                                {
                                    entry.ExtractToFile(Path.Combine(zipDir, entry.Name), true);

                                    //var responseFile = new FileUploadResult()
                                    //{
                                    //    Name = entry.Name,
                                    //    Length = entry.Length
                                    //};

                                    //result.Add(responseFile);
                                }

                                var processoUploadService = new ProcessoUploadService();

                                var processoUpload = new ProcessoUpload
                                {
                                    ProcessoID = processoID,
                                    PastaZip = zipDir,
                                    QntArq = entries.Count(),
                                    Ativo = true
                                };

                                processoUpload = processoUploadService.Insert(processoUpload);

                                processosUpload.Add(processoUpload);
                            }
                        }
                    }
                    else
                    {
                        return BadRequest("Process folder doesn't find.");
                    }
                }

                if (processosUpload.Count > 0)
                {
                    return Ok(processosUpload);
                }
                else
                {
                    return BadRequest("Something wrong happened, please try again.");
                }

                //if (result.Count > 0)
                //{
                //    return Ok(result);
                //}
                //else
                //{
                //    return NoContent();
                //}
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode(500, ex.Message);
            }
        }
    }
}
