using Dotel2.Repository.User;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;
using Dotel2.Utils;

namespace Dotel2.Service.User.Login
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _repository;
        
        public LoginService(IUserRepository repository)
        {
            _repository = repository;
            
        }

        public Models.User? AuthenticateUser(string input, string password, out string error)
        {
            error = string.Empty;

            if (!ValidateUtils.IsValidEmail(input))
            {
                error = "Định dạng tài khoản không hợp lệ.";
                return null;
            }

            var hashedPassword = ValidateUtils.HashPassword(password);
            var user = _repository.GetUserByEmailAndPassword(input, hashedPassword);

            if (user == null)
            {
                error = "Tài Khoản hoặc mật khẩu không đúng.";
                return null;
            }

            if (!user.Status)
            {
                error = "Tài khoản đã bị khóa.";
                return null;
            }

            if (user.RoleId != 2)
            {
                error = "Truy cập bị từ chối.";
                return null;
            }

            return user;
        }

    }
}
