using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Dominio;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private static readonly EmpresaService empresaService = new EmpresaService();

        // GET: api/<EmpresaController>
        [HttpGet]
        public async Task<List<Empresa>> Get()
        {
            try
            {
                return await empresaService.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<EmpresaController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var empresa = await empresaService.Get(id);

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

        // POST api/<EmpresaController>
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
                    empresa = empresaService.Insert(empresa);

                    if (empresa is null)
                    {
                        return BadRequest("Não foi possivel realizar a inserção.");
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
                    return BadRequest("IDs incompativeis.");
                }
                else
                {
                    bool exists = empresaService.Exists(id).Result;

                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        bool edited = empresaService.Edit(empresa);

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
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<EmpresaController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool exists = empresaService.Exists(id).Result;

                if (!exists)
                {
                    return NotFound();
                }
                else
                {
                    bool deleted = empresaService.Delete(id);

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
