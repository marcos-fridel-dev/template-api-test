using Application.UseCases.Location.State.Queries.GetAllStateQuery;
using Application.UseCases.Location.State.Queries.GetByCountryIdStateQuery;
using Application.UseCases.Location.State.Queries.GetByIdStateQuery;
using Asp.Versioning;
using Infrastructure.Persistence.Enums.Context;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Services.Api.Controllers.v1.Location
{
    //[Authorize(Policy = "GodRole")]
    //[Authorize(Policy = "TestRole")]
    [Route("api/v{version:apiVersion}/location/state")]
    [ApiVersion("1")]
    [ApiController]
    public class StateController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAll(int pageNumber = 1, int pageSize = 10, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted)
        {
            return Ok(await _mediator.Send(new GetAllStateUseCase(pageNumber, pageSize, isDeleted)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetByIdStateUseCase(id)));
        }

        [HttpGet("country/{countryId}")]
        public async Task<ActionResult> GetAllByCountryId(Guid countryId)
        {
            return Ok(await _mediator.Send(new GetByCountryIdStateUseCase(countryId)));
        }

    }
}