namespace Dotel2.Service.Mail
{
    public interface ISendMailService
    {
        Task<bool> SendEmailAsync(string to, string subject, string htmlContent);
    }
}
