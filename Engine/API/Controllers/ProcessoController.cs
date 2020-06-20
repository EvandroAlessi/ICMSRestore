using BLL;
using CrossCutting;
using Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/processes")]
    public class ProcessoController : ControllerBase
    {
        private static readonly ProcessoService processoService = new ProcessoService();

        // GET: api/<ProcessoController>
        [HttpGet]
        public async Task<dynamic> Get(int page = 1, int take = 30, [FromQuery] Dictionary<string, string> filters = null)
        {
            try
            {
                var response = new
                {
                    Processes = await processoService.GetAll(page, take, filters),
                    Pagination = await processoService.GetPagination(page, take, filters)
                };

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<ProcessoController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var processo = await processoService.Get(id);

                if (processo is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(processo);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<ProcessoController>
        [HttpPost]
        public IActionResult Post([FromBody] Processo processo)
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
                    var empresaService = new EmpresaService();

                    var exists = empresaService.Exists(processo.EmpresaID).Result;

                    if (exists)
                    {
                        processo = processoService.Insert(processo);

                        if (processo is null)
                        {
                            return BadRequest("Can't complete the insert process, please verify the data send.");
                        }
                        else
                        {
                            var dir = Path.Combine(AppSettings.RootPath, processo.ID.ToString());

                            PathControl.Create(dir);

                            return Ok(processo);
                        }
                    }
                    else
                    {
                        return BadRequest("The indicated Empresa does not exist in our database.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<ProcessoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Processo processo)
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
                else if (processo.ID != id)
                {
                    return BadRequest("The ID in the object is different from the indicates in the URL");
                }
                else
                {
                    var empresaService = new EmpresaService();

                    bool empExists = empresaService.Exists(processo.EmpresaID).Result;

                    if (empExists)
                    {
                        bool exists = processoService.Exists(id).Result;

                        if (!exists)
                        {
                            return NotFound();
                        }
                        else
                        {
                            var editedProcess = processoService.Edit(processo);

                            if (editedProcess is null)
                            {
                                return BadRequest("Can't complete the edit process, please verify the data send.");
                            }
                            else
                            {
                                return Ok(editedProcess);
                            }
                        }
                    }
                    else
                    {
                        return BadRequest("The indicated Empresa does not exist in our database.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<ProcessoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool exists = processoService.Exists(id).Result;

                if (!exists)
                {
                    return NotFound();
                }
                else
                {
                    bool deleted = processoService.Delete(id);

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
