
using Dotel2.Repository.User;
using Dotel2.Utils;

namespace Dotel2.Service.Admin.Auth
{
    public class AdminAuthService : IAdminAuthService
    {
        private readonly IUserRepository _userRepository;

        public AdminAuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Authenticate(string email, string password, out Models.User? user, out string message)
        {
            message = "";
            user = null;

            var hashedPassword = PasswordUtils.HashPassword(password);
            var foundUser = _userRepository.GetUserByEmailAndPassword(email, hashedPassword);

            if (foundUser == null)
            {
                message = "Tài khoản hoặc mật khẩu không đúng.";
                return false;
            }

            if (!foundUser.Status)
            {
                message = "Tài khoản đã bị khóa.";
                return false;
            }

            if (foundUser.RoleId != 1)
            {
                message = "Truy cập bị từ chối.";
                return false;
            }

            user = foundUser;
            return true;
        }
    }
}
