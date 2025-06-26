
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Dotel2.Service.Mail
{
    public class SendMailService : ISendMailService
    {
        private readonly IConfiguration _configuration;

        public SendMailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> SendEmailAsync(string to, string subject, string htmlContent)
        {
            var apiKey = _configuration["SendGrid:ApiKey"];
            var fromName = _configuration["SendGrid:FromName"];
            var fromGmail = _configuration["SendGrid:FromEmail"];
            var client = new SendGridClient(apiKey);
            var from= new EmailAddress(fromGmail, fromName);
            var toEmail = new EmailAddress(to);
            var msg = MailHelper.CreateSingleEmail(from, toEmail, subject, "", htmlContent);

            var response = await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode;
        }
    }
}
