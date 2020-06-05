using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NFeController : ControllerBase
    {
        private readonly ILogger<NFeController> _logger; 

        public NFeController(ILogger<NFeController> logger)
        {
            _logger = logger; 
        }

        // GET: api/<NFeController>
        [HttpGet]
        public async Task<IEnumerable<NFe>> Get()
        {
            return await new NFeService().GetAll();
        }

        // GET api/<NFeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<NFeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<NFeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NFeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
