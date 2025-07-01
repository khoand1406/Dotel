namespace Dotel2.Service.Admin.Auth
{
    public interface IAdminAuthService
    {
        bool Authenticate(string email, string password, out Models.User? user, out string message);
    }
}
