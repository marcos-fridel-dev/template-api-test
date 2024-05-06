using Application.Dto.Models.Security;
using Application.UseCases.Security.User.Commands.CreateUserCommand;
using Application.UseCases.Security.User.Commands.DeleteUserCommand;
using Application.UseCases.Security.User.Commands.UpdateUserCommand;
using Application.UseCases.Security.User.Queries.GetAllUserQuery;
using Application.UseCases.Security.User.Queries.GetByIdUserQuery;
using Asp.Versioning;
using Infrastructure.Persistence.Enums.Context;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Services.Api.Controllers.v1.Security
{
    [Route("api/v{version:apiVersion}/security/user")]
    [ApiVersion("1")]
    [ApiController]
    public class UserController(IMediator _mediator) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult> GetAll(int pageNumber = 1, int pageSize = 10, IsDeleted isDeleted = IsDeleted.OnlyNotDeleted)
        {
            return Ok(await _mediator.Send(new GetAllUserUseCase(pageNumber, pageSize, isDeleted)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send<UserDto>(new GetByIdUserUseCase(id)));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] UserDto role)
        {
            return Ok(await _mediator.Send(new UpdateUserUseCase(id, role)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteUserUseCase(id)));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] UserDto role)
        {
            return Ok(await _mediator.Send(new CreateUserUseCase(role)));
        }
    }
}