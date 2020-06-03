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
using Models;

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
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            try
            {
                var result = new List<FileUploadResult>();

                foreach (var file in files)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FileName);

                    if (!System.IO.File.Exists(path))
                    {
                        if (file.FileName.EndsWith(".xml"))
                        {
                            using (var fileStream = new FileStream(path, FileMode.Create))
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
                            using (var stream = file.OpenReadStream())
                            {
                                using (ZipArchive archive = new ZipArchive(stream))
                                {
                                    var dir = Path.ChangeExtension(path, null);

                                    PathControl.GrantAccess(dir);

                                    foreach (ZipArchiveEntry entry in archive.Entries)
                                    {
                                        var filePath = Path.Combine(dir, entry.FullName);

                                        if (!System.IO.File.Exists(filePath))
                                        {
                                            if (!dir.EndsWith(filePath))
                                                continue;

                                            if (string.IsNullOrEmpty(Path.GetExtension(entry.FullName)))
                                            {
                                                PathControl.GrantAccess(filePath);
                                            }
                                            else
                                            {
                                                entry.ExtractToFile(Path.Combine(dir, entry.FullName));

                                                var responseFile = new FileUploadResult()
                                                {
                                                    Name = entry.FullName,
                                                    Length = entry.Length
                                                };

                                                result.Add(responseFile);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        // GET api/<UploadController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UploadController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<UploadController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UploadController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
