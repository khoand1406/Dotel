
using Dotel2.Repository.User;
using Dotel2.Utils;

namespace Dotel2.Service.User.Profile
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserRepository _userRepository;

        public UserProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Models.User getUserById(int id)
        {
            return _userRepository.getUserbyRentalId(id);
        }

        public bool UpdateUserProfile(int userId, string fullname, string mainPhone, string secondaryPhone, string email, out string errorMessage)
        {
            errorMessage = "";

            if (!ValidateUtils.IsValidEmail(email))
            {
                errorMessage = "Email không hợp lệ.";
                return false;
            }

            if (!ValidateUtils.IsValidPhone(mainPhone))
            {
                errorMessage = "Số điện thoại chính không hợp lệ.";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(secondaryPhone) && !ValidateUtils.IsValidPhone(secondaryPhone))
            {
                errorMessage = "Số điện thoại phụ không hợp lệ.";
                return false;
            }

            var user = _userRepository.getUserbyRentalId(userId);
            if (user == null)
            {
                errorMessage = "Không tìm thấy người dùng.";
                return false;
            }

            user.Fullname = fullname;
            user.MainPhoneNumber = mainPhone;
            user.SecondaryPhoneNumber = secondaryPhone;
            user.Email = email;

            _userRepository.UpdateUserProfile(user);
            return true;
        }
    }
}
