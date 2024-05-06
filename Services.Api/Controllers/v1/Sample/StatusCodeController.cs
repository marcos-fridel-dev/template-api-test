using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Services.Api.Controllers.v1.Sample
{
    [Route("api/v{version:apiVersion}/sample/status-code")]
    [ApiVersion("1")]
    [ApiController]
    public class StatusCodeController() : ControllerBase
    {

        [HttpGet("{statusCode}")]
        public async Task<ActionResult> GetStatusCode(int statusCode)
        {
            return StatusCode(statusCode);
        }
    }
}