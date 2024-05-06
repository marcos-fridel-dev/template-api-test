using Application.Dto.Models.Services.Mail;
using Application.UseCases.Services.Mail.Commands.SendMailSampleCommand;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Services.Api.Controllers.v1.Sample
{
    [Route("api/v{version:apiVersion}/sample/mail")]
    [ApiVersion("1")]
    [ApiController]
    [Authorize]
    public class MailController(IMediator _mediator) : Controller
    {

        [HttpPost("send")]
        public async Task<ActionResult<bool>> Send(SendMailServiceRequestDto request)
        {
            return Ok(await _mediator.Send(new SendMailServiceUseCase(request)));
        }
    }
}