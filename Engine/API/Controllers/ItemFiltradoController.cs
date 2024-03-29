﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BLL;
using CrossCutting;
using Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/filtered-items")]
    public class ItemFiltradoController : ControllerBase
    {
        private static readonly ItemFiltradoService service = new ItemFiltradoService();

        // GET: api/<ItemFiltradoController>
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<dynamic> Get(int page = 1, int take = 30, [FromQuery] Dictionary<string, string> filters = null)
        {
            try
            {
                var response = new
                {
                    FilteredItems = await service.GetAll(page, take, filters),
                    Pagination = await service.GetPagination(page, take, filters)
                };

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("/api/processes/{processID}/filtered-items")]
        [HttpGet]
        public async Task<dynamic> GetAllByProcess(int processID, int page = 1, int take = 30)
        {
            try
            {
                Dictionary<string, string> filters = new Dictionary<string, string>();

                filters.Add("ProcessoID", processID.ToString());

                var response = new
                {
                    FilteredItems = await service.GetAll(processID, page, take),
                    Pagination = await service.GetPagination(page, take, filters)
                };

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<ItemFiltradoController>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var itemFiltrado = await service.Get(id);

                if (itemFiltrado is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(itemFiltrado);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            try
            {
                return Ok(await service.GetCount());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("summary-result")]
        public async Task<IActionResult> GetSummary(int processID)
        {
            try
            {
                return Ok(await service.GetSummary(processID));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public static void CreateZipFile(string fileName, IEnumerable<string> files)
        {
            // Create and open a new ZIP file
            var zip = ZipFile.Open(fileName, ZipArchiveMode.Create);

            foreach (var file in files)
            {
                // Add the entry for each file
                zip.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
            }

            // Dispose of the object when we are  done
            zip.Dispose();
        }

        [HttpGet("download-results")]
        public async Task<IActionResult> Download(int processID, int? ncm = null)
        {
            try
            {
                var path = $"{ Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) }\\FILES";

                PathControl.Create(path);

                path += $"\\{ processID }";

                PathControl.Create(path);
                
                var tempFile = Path.Combine(Path.GetTempPath(), $"{ Guid.NewGuid() }.zip"); // might want to clean this up if there are a lot of downloads

                await service.BuildResult(path, processID, ncm);

                using (var stream = new FileStream(tempFile, FileMode.Create))
                {
                    using (var archive = new ZipArchive(stream, ZipArchiveMode.Update, true))
                    {
                        if (ncm is null)
                        {
                            foreach (var item in Directory.GetDirectories(path))
                            {
                                //Create a zip entry for each attachment
                                foreach (var file in Directory.GetFiles(item))
                                {
                                    // Add the entry for each file
                                    archive.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
                                }
                            }
                        }
                        else
                        {
                            path = Path.Combine(path, ncm.ToString());

                            foreach (var file in Directory.GetFiles(path))
                            {
                                // Add the entry for each file
                                archive.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
                            }
                        }
                    }
                }

                const string contentType = "application/zip";
                HttpContext.Response.ContentType = contentType;

                var result = new FileContentResult(System.IO.File.ReadAllBytes(tempFile), contentType)
                {
                    FileDownloadName = $@"{ (ncm is null 
                                                ? tempFile 
                                                : ncm.ToString()) }.zip"
                };

                System.IO.File.Delete(tempFile);

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<ItemFiltradoController>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemFiltrado"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] ItemFiltrado itemFiltrado)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    List<string> errors = new List<string>();

                    foreach (var state in ModelState.Values)
                    {
                        errors.AddRange(state.Errors.Select(x => x.ErrorMessage).Where(x => !string.IsNullOrEmpty(x)));
                    }

                    return BadRequest(errors);
                }
                else
                {
                    itemFiltrado = service.Insert(itemFiltrado);

                    if (itemFiltrado is null)
                    {
                        return BadRequest("Can't complete the insert process, please verify the data send.");
                    }
                    else
                    {
                        return Ok(itemFiltrado);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<ItemFiltradoController>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemFiltrado"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ItemFiltrado itemFiltrado)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    List<string> errors = new List<string>();

                    foreach (var state in ModelState.Values)
                    {
                        errors.AddRange(state.Errors.Select(x => x.ErrorMessage).Where(x => !string.IsNullOrEmpty(x)));
                    }

                    return BadRequest(errors);
                }
                else if (itemFiltrado.ID != id)
                {
                    return BadRequest("The ID in the object is different from the indicates in the URL.");
                }
                else
                {
                    bool exists = service.Exists(itemFiltrado.ID).Result;

                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        var editedItemFiltrado = service.Edit(itemFiltrado);

                        if (editedItemFiltrado is null)
                        {
                            return BadRequest("Can't complete the edit process, please verify the data send.");
                        }
                        else
                        {
                            return Ok(editedItemFiltrado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<ItemFiltradoController>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool exists = service.Exists(id).Result;

                if (!exists)
                {
                    return NotFound();
                }
                else
                {
                    bool deleted = service.Delete(id);

                    if (deleted)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest("Can't complete the delete process, please verify the data send.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
