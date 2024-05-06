using Application.UseCases.Sample.Http.Pokemon.Queries.GetAllPokemonHttpQuery;
using Application.UseCases.Sample.Http.Pokemon.Queries.GetByIdPokemonHttpQuery;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Services.Api.Controllers.v1.Sample
{
    [Route("api/v{version:apiVersion}/sample/http/pokemon")]
    [ApiVersion("1")]
    [ApiController]
    public class HttpPokemonController(IMediator _mediator) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllPokemonHttpUseCase()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetByIdPokemonHttpUseCase(id)));
        }
    }
}