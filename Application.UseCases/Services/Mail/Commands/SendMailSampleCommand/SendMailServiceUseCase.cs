using Application.Dto.Models.Services.Mail;
using MediatR;

namespace Application.UseCases.Services.Mail.Commands.SendMailSampleCommand
{
    public sealed class SendMailServiceUseCase(SendMailServiceRequestDto request) : IRequest<SendMailServiceResponseDto>
    {
        public SendMailServiceRequestDto Mail = request;
    }
}