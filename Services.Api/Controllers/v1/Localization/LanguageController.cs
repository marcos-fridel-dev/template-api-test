using Application.UseCases.Localization.Language.Queries.GetAllLanguageAvailableQuery;
using Asp.Versioning;
using Infrastructure.Services.Extensions.Cache;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;

namespace Services.Api.Controllers.v1.Localization
{
    [Route("api/v{version:apiVersion}/localization/language")]
    [ApiVersion("1")]
    [ApiController]
    public class LanguageController(IMediator _mediator, IDistributedCache _chache) : ControllerBase
    {
        [HttpGet("availables")]
        public async Task<ActionResult> GetAllCache()
        {
            return Ok(await _chache.GetCacheAsync(
                "LanguageAvailable", async () => await _mediator.Send(new GetAllLanguageAvailableUseCase())));
        }
    }
}