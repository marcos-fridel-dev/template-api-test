using Application.Dto.Models.Security;
using Application.UseCases.Security.Role.Commands.CreateRoleCommand;
using Application.UseCases.Security.Role.Commands.DeleteRoleCommand;
using Application.UseCases.Security.Role.Commands.UpdateRoleCommand;
using Application.UseCases.Security.Role.Queries.GetAllRoleQuery;
using Application.UseCases.Security.Role.Queries.GetByIdRoleQuery;
using Asp.Versioning;
using Infrastructure.Persistence.Enums.Context;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Services.Api.Controllers.v1.Security
{
    [Route("api/v{version:apiVersion}/security/role")]
    [ApiVersion("1")]
    [ApiController]
    public class RoleController(IMediator _mediator) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult> GetAll(int pageNumber = 1, int pageSize = 10, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted)
        {
            return Ok(await _mediator.Send(new GetAllRoleUseCase(pageNumber, pageSize, isDeleted)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send<RoleDto>(new GetByIdRoleUseCase(id)));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] RoleDto role)
        {
            return Ok(await _mediator.Send(new UpdateRoleUseCase(id, role)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteRoleUseCase(id)));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] RoleDto role)
        {
            return Ok(await _mediator.Send(new CreateRoleUseCase(role)));
        }
    }
}