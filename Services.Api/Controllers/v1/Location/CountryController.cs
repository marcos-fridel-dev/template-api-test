using Application.UseCases.Location.Country.Queries.GetAllCountryQuery;
using Application.UseCases.Location.Country.Queries.GetByIdCountryQuery;
using Asp.Versioning;
using Infrastructure.Persistence.Enums.Context;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Services.Api.Controllers.v1.Location
{
    [Route("api/v{version:apiVersion}/location/country")]
    [ApiVersion("1")]
    [ApiController]
    public class CountryController(IMediator _mediator) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult> GetAll(int pageNumber = 1, int pageSize = 10, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted)
        {
            return Ok(await _mediator.Send(new GetAllCountryUseCase(pageNumber, pageSize, isDeleted)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetByIdCountryUseCase(id)));
        }

    }
}