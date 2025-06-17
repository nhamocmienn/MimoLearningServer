using MailKit.Net.Smtp;
using MimeKit;

namespace WebAPI.Helpers
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(
                _config["SmtpSettings:SenderName"],
                _config["SmtpSettings:SenderEmail"]
            ));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect(_config["SmtpSettings:Server"], int.Parse(_config["SmtpSettings:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_config["SmtpSettings:SenderEmail"], _config["SmtpSettings:SenderPassword"]);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
