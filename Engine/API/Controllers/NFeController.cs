using BLL;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/invoices")]
    [ApiController]
    public class NFeController : ControllerBase
    {
        private static readonly NFeService nfeService = new NFeService();

        // GET: api/<NFeController>
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
                    Invoices = await nfeService.GetAll(page, take, filters),
                    Pagination = await nfeService.GetPagination(page, take, filters)
                };

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("simplify")]
        public async Task<dynamic> GetSimplify(int page = 1, int take = 30, [FromQuery] Dictionary<string, string> filters = null)
        {
            try
            {
                var response = new
                {
                    Invoices = await nfeService.GetAllSimplify(page, take, filters),
                    Pagination = await nfeService.GetPagination(page, take, filters)
                };

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<NFeController>/5
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
                var nfe = await nfeService.Get(id);

                if (nfe is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(nfe);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<NFeController>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nfe"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] NFe nfe)
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
                    nfe = nfeService.Insert(nfe);

                    if (nfe is null)
                    {
                        return BadRequest("Can't complete the insert process, please verify the data send.");
                    }
                    else
                    {
                        return Ok(nfe);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<NFeController>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nfe"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] NFe nfe)
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
                else if (nfe.ID != id)
                {
                    return BadRequest("The ID in the object is different from the indicates in the URL.");
                }
                else
                {
                    bool exists = nfeService.Exists(id).Result;

                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        bool edited = nfeService.Edit(nfe);

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

        // DELETE api/<NFeController>/5
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
                bool exists = nfeService.Exists(id).Result;

                if (!exists)
                {
                    return NotFound();
                }
                else
                {
                    bool deleted = nfeService.Delete(id);

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
