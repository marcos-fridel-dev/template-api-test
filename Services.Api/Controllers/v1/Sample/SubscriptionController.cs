using Application.Services.Sample.Subscription.Models;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Services.Api.Controllers.v1.Sample
{
    [Route("api/v{version:apiVersion}/sample/subscription")]
    [ApiVersion("1")]
    [ApiController]
    //public class SubscriptionController(QueueBasicSubscriptionService _subscriptionService) : ControllerBase
    public class SubscriptionController() : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] QueueBasicSampleEvent request)
        {
            //await _subscriptionService.Publish(request);
            return Ok();
        }
    }
}