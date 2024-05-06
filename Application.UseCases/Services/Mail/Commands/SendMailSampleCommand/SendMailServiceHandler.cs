using Application.Dto.Models.Services.Mail;
using AutoMapper;
using Infrastructure.Services.Interfaces.Mail;
using Infrastructure.Services.Models.Mail;
using MediatR;
using Microsoft.Extensions.Localization;
using Shared.Localization.Resources.Languages;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Services.Mail.Commands.SendMailSampleCommand
{
    public class SendMailServiceHandler(IMailService _mailService, IStringLocalizer _localizer) : IRequestHandler<SendMailServiceUseCase, SendMailServiceResponseDto>
    {

        public async Task<SendMailServiceResponseDto> Handle(SendMailServiceUseCase request, CancellationToken cancellationToken)
        {

            SendMailResponse result = await _mailService.SendAsync(
                //request.Mail.From,
                //request.Mail.FromDisplay,
                request.Mail.Subject,
                request.Mail.Body,
                request.Mail.To,
                request.Mail.Cc,
                request.Mail.Bcc
            );

            return
                new()
                {
                    Success = result.Success,
                    Message = _localizer.GetString(result.Success ? Language.EmailSentSuccessfully : Language.EmailCouldNotBeSent),
                    MessageError = result.Success ? "" : result.Message
                };
        }
    }
}