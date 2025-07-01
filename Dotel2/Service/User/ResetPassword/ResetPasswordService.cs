using Dotel2.Repository.User;
using System.Security.Cryptography;
using System.Text;

namespace Dotel2.Service.User.ResetPassword
{
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly IUserRepository _userRepository;

        public ResetPasswordService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool ResetPassword(string email, string newPassword, string repeatPassword, out string message)
        {
            message = "";

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(repeatPassword))
            {
                message = "Không được để trống mật khẩu";
                return false;
            }

            if (newPassword != repeatPassword)
            {
                message = "Mật khẩu không khớp";
                return false;
            }

            var user = _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                message = "Email sai định dạng hoặc không tồn tại";
                return false;
            }

            var hashed = HashPassword(newPassword);
            _userRepository.UpdateUserPassword(email, hashed);

            return true;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
