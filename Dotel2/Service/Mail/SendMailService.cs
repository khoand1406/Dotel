
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
            
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                Console.WriteLine("❌ SendGrid API Key is missing!");
                return false;
            }
            var client = new SendGridClient(apiKey);
            var from= new EmailAddress(fromGmail, fromName);
            var toEmail = new EmailAddress(to);
            var msg = MailHelper.CreateSingleEmail(from, toEmail, subject, "", htmlContent);

            var response = await client.SendEmailAsync(msg);
            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Body.ReadAsStringAsync();
                Console.WriteLine($"❌ SendGrid failed: {response.StatusCode}, {errorBody}");
            }
            return response.IsSuccessStatusCode;
        }
    }
}
