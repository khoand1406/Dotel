namespace Dotel2.Service.User.ResetPassword
{
    public interface IResetPasswordService
    {
        bool ResetPassword(string email, string newPassword, string repeatPassword, out string message);
    }
}
