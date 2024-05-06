using Application.Dto.Models.Location;
using Application.UseCases.Location.Currency.Commands.CreateCurrencyCommand;
using Application.UseCases.Location.Currency.Commands.DeleteCurrencyCommand;
using Application.UseCases.Location.Currency.Commands.UpdateCurrencyCommand;
using Application.UseCases.Location.Currency.Queries.GetAllCurrencyQuery;
using Application.UseCases.Location.Currency.Queries.GetByCountryIdCurrencyQuery;
using Application.UseCases.Location.Currency.Queries.GetByIdCurrencyQuery;
using Asp.Versioning;
using Infrastructure.Persistence.Enums.Context;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Services.Api.Controllers.v1.Location
{
    [Route("api/v{version:apiVersion}/location/currency")]
    [ApiVersion("1")]
    [ApiController]
    [Authorize]
    public class CurrencyController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAll(int pageNumber = 1, int pageSize = 10, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted)
        {
            return Ok(await _mediator.Send(new GetAllCurrencyUseCase(pageNumber, pageSize, isDeleted)));
        }

        [HttpGet("country/{countryId}")]
        public async Task<ActionResult> GetAllByCountryId(Guid countryId)
        {
            return Ok(await _mediator.Send(new GetByCountryIdCurrencyUseCase(countryId)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send<CurrencyDto>(new GetByIdCurrencyUseCase(id)));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] CurrencyPostDto currency)
        {
            return Ok(await _mediator.Send(new UpdateCurrencyUseCase(id, currency)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteCurrencyUseCase(id)));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CurrencyPostDto currency)
        {
            return Ok(await _mediator.Send(new CreateCurrencyUseCase(currency)));

        }
    }
}