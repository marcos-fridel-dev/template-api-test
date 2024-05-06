using Application.UseCases.Location.City.Queries.GetAllCityQuery;
using Application.UseCases.Location.City.Queries.GetByIdCityQuery;
using Application.UseCases.Location.City.Queries.GetByStateIdCityQuery;
using Asp.Versioning;
using Infrastructure.Persistence.Enums.Context;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Services.Api.Controllers.v1.Location
{
    //[Authorize(Policy = "TestRole")]
    [Route("api/v{version:apiVersion}/location/city")]
    [ApiVersion("1")]
    [ApiController]
    public class CityController(IMediator _mediator) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult> GetAll(int pageNumber = 1, int pageSize = 10, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted)
        {
            return Ok(await _mediator.Send(new GetAllCityUseCase(pageNumber, pageSize, isDeleted)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetByIdCityUseCase(id)));
        }

        [HttpGet("state/{stateId}")]
        public async Task<ActionResult> GetAllByStateId(Guid stateId)
        {
            return Ok(await _mediator.Send(new GetByStateIdCityUseCase(stateId)));
        }
    }
}