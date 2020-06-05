using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading.Tasks;
using CrossCutting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Models;

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

        /// <summary>
        /// POST: api/<UploadController>
        /// </summary>
        /// <param name="files">Arquivos: XML e/ou ZIP</param>
        /// <param name="id">ID do processo</param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm]List<IFormFile> files, int id)
        {
            try
            {
                var result = new List<FileUploadResult>();

                foreach (var file in files)
                {
                    var path = Path.Combine(AppSettings.RootPath, id.ToString());

                    if (Directory.Exists(path))
                    {
                        if (file.FileName.EndsWith(".xml"))
                        {
                            var filePath = Path.Combine(path, file.FileName);

                            if (!System.IO.File.Exists(filePath))
                            {
                                using (var fileStream = new FileStream(filePath, FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream);

                                    var responseFile = new FileUploadResult()
                                    {
                                        Name = file.FileName,
                                        Length = file.Length
                                    };

                                    result.Add(responseFile);
                                }
                            }
                            else
                            {
                                return BadRequest("Arquivo já se encontrada em nosso servidor.");
                            }
                        }
                        else
                        {
                            using (var stream = file.OpenReadStream())
                            {
                                using (ZipArchive archive = new ZipArchive(stream))
                                {
                                    var zipDir = Path.Combine(path, Path.ChangeExtension(file.FileName, null));

                                    PathControl.Create(zipDir);

                                    // One By One Code
                                    foreach (ZipArchiveEntry entry in archive.Entries.Where(x => !string.IsNullOrWhiteSpace(x.Name)))
                                    {
                                        entry.ExtractToFile(Path.Combine(zipDir, entry.Name), true);

                                        var responseFile = new FileUploadResult()
                                        {
                                            Name = entry.Name,
                                            Length = entry.Length
                                        };

                                        result.Add(responseFile);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        return BadRequest("Pasta de Processo não encontrada.");
                    }
                }

                if (result.Count > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode(500, ex.Message);
            }
        }
    }
}
