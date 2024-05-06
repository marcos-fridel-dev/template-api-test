using Application.UseCases.Sample.Queue.Queries.GetQueueBasicSampleQuery;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Services.Api.Controllers.v1.Sample
{
    [Route("api/v{version:apiVersion}/sample/queue")]
    [ApiVersion("1")]
    [ApiController]
    public class QueueController(IMediator _mediator) : ControllerBase
    {

        [Route("basic")]
        [HttpGet]
        public async Task<ActionResult> QueueBasicGet()
        {
            await _mediator.Send(new GetQueueBasicSampleUseCase());
            return Ok();
        }
    }
}