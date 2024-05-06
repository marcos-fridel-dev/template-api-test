using Application.Dto.Models.Authentication;
using Application.UseCases.Authentication.Commands.LoginAuthenticationCommand;
using Application.UseCases.Authentication.Commands.RefreshTokenAuthenticationCommand;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Services.Api.Controllers.v1.Authentication
{

    [Route("api/v{version:apiVersion}/login")]
    [ApiVersion("1")]
    [ApiController]
    public class LoginController(IMediator _mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginRequestDto login)
        {
            return Ok(await _mediator.Send(new LoginAuthenticationUseCase(login)));
        }

        [HttpGet("refresh-token")]
        public async Task<ActionResult> RefreshToken()
        {
            string? token = HttpContext.Request.Headers["Authorization"];
            token = token ?? "";

            token = token.Substring(7);

            return Ok(await _mediator.Send(new RefreshTokenAuthenticationUseCase(token)));
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult> GetById(Guid id)
        //{
        //    var result = await _mediator.Send(new CityGetByIdUseCase(id));
        //    return Ok(result);
        //}

        //[HttpGet("state/{stateId}")]
        //public async Task<ActionResult> GetAllByStateId(Guid stateId)
        //{
        //    var result = await _mediator.Send(new CityGetByStateIdUseCase(stateId));
        //    return Ok(result);
        //}
    }
}