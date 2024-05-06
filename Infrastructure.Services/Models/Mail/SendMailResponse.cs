namespace Infrastructure.Services.Models.Mail
{
    public class SendMailResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string From { get; set; }
        public string FromDisplay { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public string? Cc { get; set; }
        public string? Bcc { get; set; }
    }
}
