namespace Application.Dto.Models.Services.Mail
{
    public sealed class SendMailServiceRequestDto
    {
        public string From { get; init; }
        public string FromDisplay { get; init; }
        public string To { get; init; }
        public string Subject { get; init; }
        public string Body { get; init; }
        public string? Cc { get; init; }
        public string? Bcc { get; init; }
    }

    public sealed class SendMailServiceResponseDto
    { 
        public bool Success { get; init; }
        public string Message { get; init; } 
        public string? MessageError { get; init; }

    }
}
