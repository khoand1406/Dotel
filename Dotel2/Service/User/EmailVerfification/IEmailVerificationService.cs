namespace Dotel2.Service.User.EmailVerfification
{
    public interface IEmailVerificationService
    {
        bool ValidateCode(Models.User user, string code, out string errorMessage);
    }
}
