using BLL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/metrics")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private static readonly MetricsService service = new MetricsService();

        [HttpGet("counts")]
        public async Task<IActionResult> GetCounts()
        {
            try
            {
                return Ok(await service.GetCounts());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
