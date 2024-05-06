using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Services.Api.Controllers.v1.Sample
{
    [Route("api/v{version:apiVersion}/sample/wait")]
    [ApiVersion("1")]
    [ApiController]
    public class WaitController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            await Task.Delay(3000);
            return Ok();
        }

        [HttpGet("{milleseconds}")]
        public async Task<ActionResult> Get(int milleseconds)
        {
            await Task.Delay(milleseconds);
            return Ok();
        }
    }
}