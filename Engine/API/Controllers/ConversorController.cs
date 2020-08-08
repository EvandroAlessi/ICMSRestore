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
    [Route("api/converters")]
    public class ConversorController : ControllerBase
    {
        private static readonly ConversorService service = new ConversorService();

        // GET: api/<ConversorController>
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
                    Converters = await service.GetAll(page, take, filters),
                    Pagination = await service.GetPagination(page, take, filters)
                };

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        [HttpGet("unit-differences")]
        public async Task<List<Conversor>> GetUnitDifferences(int processID)
        {
            try
            {
                return await service.GetUnitDifferences(processID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<ConversorController>/5
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
                var conversor = await service.Get(id);

                if (conversor is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(conversor);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        // POST api/<ConversorController>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversor"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] Conversor conversor)
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
                    var companyService = new EmpresaService();

                    bool companyExists = companyService.Exists(conversor.EmpresaID).Result;

                    if (companyExists)
                    {
                        conversor = service.Insert(conversor);

                        if (conversor is null)
                        {
                            return BadRequest("Can't complete the insert process, please verify the data send.");
                        }
                        else
                        {
                            return Ok(conversor);
                        }
                    }
                    else
                    {
                        return BadRequest("The indicated company does not exists.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<ConversorController>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="conversor"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Conversor conversor)
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
                else if (conversor.ID != id)
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
                        var companyService = new EmpresaService();

                        bool companyExists = companyService.Exists(conversor.EmpresaID).Result;

                        if (companyExists)
                        {
                            var editedConverter = service.Edit(conversor);

                            if (editedConverter is null)
                            {
                                return BadRequest("Can't complete the edit process, please verify the data send.");
                            }
                            else
                            {
                                return Ok(editedConverter);
                            }
                        }
                        else
                        {
                            return BadRequest("The indicated company does not exists.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<ConversorController>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
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
