using Dotel2.Models;

namespace Dotel2.Service.User.Profile
{
    public interface IUserProfileService
    {
        public Models.User getUserById(int id);

        bool UpdateUserProfile(int userId, string fullname, string mainPhone, string secondaryPhone, string email, out string errorMessage);
    }
}
