using BLL;
using Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/companies")]
    public class EmpresaController : ControllerBase
    {
        private static readonly EmpresaService service = new EmpresaService();

        // GET: api/<EmpresaController>
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
                    Companies = await service.GetAll(page, take, filters),
                    Pagination = await service.GetPagination(page, take, filters)
                };

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<EmpresaController>/5
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
                var empresa = await service.Get(id);

                if (empresa is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(empresa);
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

        // POST api/<EmpresaController>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="empresa"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] Empresa empresa)
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
                    empresa = service.Insert(empresa);

                    if (empresa is null)
                    {
                        return BadRequest("Can't complete the insert process, please verify the data send.");
                    }
                    else
                    {
                        return Ok(empresa);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<EmpresaController>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="empresa"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Empresa empresa)
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
                else if (empresa.ID != id)
                {
                    return BadRequest("The ID in the object is different from the indicates in the URL.");
                }
                else
                {
                    bool exists = service.Exists(id).Result;

                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        var editedCompany = service.Edit(empresa);

                        if (editedCompany is null)
                        {
                            return BadRequest("Can't complete the edit process, please verify the data send.");
                        }
                        else
                        {
                            return Ok(editedCompany);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<EmpresaController>/5
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
