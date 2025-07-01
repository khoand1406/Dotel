namespace Dotel2.Service.User.Register
{
    public interface IRegisterService
    {
        (bool Success, string ErrorMessage) Register(string emailOrPhone, string password, string repeatPassword, string fullName);
    }
}

