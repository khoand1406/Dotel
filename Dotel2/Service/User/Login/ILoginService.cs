namespace Dotel2.Service.User.Login
{
    public interface ILoginService
    {
        Models.User? AuthenticateUser(string input, string password, out string error);
        
    }
}
