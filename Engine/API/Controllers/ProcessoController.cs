using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using CrossCutting;
using Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessoController : ControllerBase
    {
        private static readonly ProcessoService processoService = new ProcessoService();

        // GET: api/<ProcessoController>
        [HttpGet]
        public async Task<List<Processo>> Get()
        {
            try
            {
                return await processoService.GetAll();
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

                    var exists = empresaService.Exists(processo.Empresa.ID).Result;

                    if (exists)
                    {
                        processo = processoService.Insert(processo);

                        if (processo is null)
                        {
                            return BadRequest("Não foi possivel realizar a inserção.");
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
                        return BadRequest("A Empresa indicada não existe em nossa base.");
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
                    return BadRequest("IDs incompativeis.");
                }
                else
                {
                    var empresaService = new EmpresaService();

                    bool empExists = empresaService.Exists(processo.Empresa.ID).Result;

                    if (empExists)
                    {
                        bool exists = processoService.Exists(id).Result;

                        if (!exists)
                        {
                            return NotFound();
                        }
                        else
                        {
                            bool edited = processoService.Edit(processo);

                            if (edited)
                            {
                                return NoContent();
                            }
                            else
                            {
                                return BadRequest("Não foi possivel realizar a alteração.");
                            }
                        }
                    }
                    else
                    {
                        return BadRequest("A Empresa indicada não existe em nossa base.");
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
                        return BadRequest("Não foi possivel deletar.");
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
