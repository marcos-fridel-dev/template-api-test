using Application.Dto.Models.Sample;
using Application.UseCases.Sample.Entity.Commands;
using Application.UseCases.Sample.Entity.Queries;
using Asp.Versioning;
using Infrastructure.Services.Extensions.Cache;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Services.Api.Controllers.v1.Sample
{
    [Route("api/v{version:apiVersion}/sample/entity")]
    [ApiVersion("1")]
    [ApiController]
    public class EntityController(IMediator _mediator, IDistributedCache _chache) : ControllerBase
    {

        [HttpGet("cache")]
        public async Task<ActionResult> GetAllCache()
        {
            return Ok(await _chache.GetCacheAsync(
                "EntityGetAll", async () => await _mediator.Send(new EntityGetAllUseCase())));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _mediator.Send(new EntityGetAllUseCase());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new EntityGetByIdUseCase(id));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] EntityPostDto entity)
        {
            var result = await _mediator.Send(new EntityUpdateUseCase(id, entity));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new EntityDeleteUseCase(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] EntityPostDto entity)
        {
            var result = await _mediator.Send(new EntityCreateUseCase(entity));
            return Ok(result);

        }

    }
}
