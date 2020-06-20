using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/filtered-items")]
    public class ItensFiltradosController : ControllerBase
    {
        private static readonly ItemFiltradoService itemFiltradoService = new ItemFiltradoService();

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
                    FilteredItems = await itemFiltradoService.GetAll(page, take, filters),
                    Pagination = await itemFiltradoService.GetPagination(page, take, filters)
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
                    FilteredItems = await itemFiltradoService.GetAll(processID, page, take),
                    Pagination = await itemFiltradoService.GetPagination(page, take, filters)
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
                var itemFiltrado = await itemFiltradoService.Get(id);

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
                    itemFiltrado = itemFiltradoService.Insert(itemFiltrado);

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
                    bool exists = itemFiltradoService.Exists(itemFiltrado.ID).Result;

                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        var editedItemFiltrado = itemFiltradoService.Edit(itemFiltrado);

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
                bool exists = itemFiltradoService.Exists(id).Result;

                if (!exists)
                {
                    return NotFound();
                }
                else
                {
                    bool deleted = itemFiltradoService.Delete(id);

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
