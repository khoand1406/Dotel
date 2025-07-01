using Dotel2.Repository.User;
using Dotel2.Utils;

namespace Dotel2.Service.User.Register
{
    public class RegisterService : IRegisterService
    {
        private IUserRepository _userRepository;
        

        public RegisterService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            
        }


        public (bool Success, string ErrorMessage) Register(string input, string password, string repeatPassword, string fullName)
        {
            input = input.Trim().ToLower();
            fullName = fullName.Trim();

            if (!ValidateUtils.IsValidEmail(input))
            {
                return (false, "Định dạng email không hợp lệ.");
            }

            var existingUser = _userRepository.GetUserByEmail(input);
            if (existingUser != null)
            {
                return (false, "Email đã tồn tại.");
            }

            if (password != repeatPassword)
            {
                return (false, "Mật khẩu không khớp.");
            }

            var hashedPassword = ValidateUtils.HashPassword(password);

            var newUser = new Models.User
            {
                Fullname = fullName,
                Password = hashedPassword,
                RoleId = 2,
                Status = true,
                Email = input,
                CheckEmail = true
            };

            _userRepository.RegisterUser(newUser);

            return (true, "Đăng ký thành công.");
        }
    }
}
