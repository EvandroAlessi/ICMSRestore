using BLL;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessoUploadController : ControllerBase
    {
        private static readonly ProcessoUploadService processoUploadService = new ProcessoUploadService();

        // GET: api/<ProcessoUploadController>
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
                    UploadProcesses = await processoUploadService.GetAll(page, take, filters),
                    Pagination = await processoUploadService.GetPagination(take, filters)
                };

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<ProcessoUploadController>/5
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
                var processoUpload = await processoUploadService.Get(id);

                if (processoUpload is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(processoUpload);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("state/{id}")]
        public async Task<IActionResult> GetState(int id)
        {
            try
            {
                var processoUpload = await processoUploadService.Get(id);

                if (processoUpload is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(processoUploadService.GetState(processoUpload));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<ProcessoUploadController>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="processoUpload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] ProcessoUpload processoUpload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    List<string> errors = new List<string>();

                    foreach (var item in ModelState.Values)
                    {
                        errors.AddRange(item.Errors.Select(x => x.ErrorMessage).Where(x => !string.IsNullOrEmpty(x)));
                    }

                    return BadRequest(errors);
                }
                else
                {
                    processoUpload = processoUploadService.Insert(processoUpload);

                    if (processoUpload is null)
                    {
                        return BadRequest("Can't complete the insert process, please verify the data send.");
                    }
                    else
                    {
                        return Ok(processoUpload);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<ProcessoUploadController>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="processoUpload"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProcessoUpload processoUpload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    List<string> errors = new List<string>();

                    foreach (var item in ModelState.Values)
                    {
                        errors.AddRange(item.Errors.Select(x => x.ErrorMessage).Where(x => !string.IsNullOrEmpty(x)));
                    }

                    return BadRequest(errors);
                }
                else if (processoUpload.ID != id)
                {
                    return BadRequest("The ID in the object is different from the indicates in the URL.");
                }
                else
                {
                    bool exists = processoUploadService.Exists(id).Result;

                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        bool edited = processoUploadService.Edit(processoUpload);

                        if (edited)
                        {
                            return NoContent();
                        }
                        else
                        {
                            return BadRequest("Can't complete the edit process, please verify the data send.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<ProcessoUploadController>/5
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
                bool exists = processoUploadService.Exists(id).Result;

                if (!exists)
                {
                    return NotFound();
                }
                else
                {
                    bool deleted = processoUploadService.Delete(id);

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
