
using Dotel2.Repository.User;

namespace Dotel2.Service.User.EmailVerfification
{
    public class EmailVerificationService : IEmailVerificationService
    {
        private readonly IUserRepository _userRepository;

        public EmailVerificationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool ValidateCode(Models.User user, string code, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(code))
            {
                errorMessage = "Vui lòng nhập mã xác thực.";
                return false;
            }

            if (user.EmailVerificationCode != code || user.EmailVerificationCodeExpires < DateTime.Now)
            {
                errorMessage = "Mã xác thực không hợp lệ hoặc đã hết hạn.";
                return false;
            }

            user.CheckEmail = true;
            user.EmailVerificationCode = null;
            user.EmailVerificationCodeExpires = null;
            _userRepository.UpdateUserProfile(user);

            return true;
        }
    }
}
