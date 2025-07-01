using Dotel2.DTOs;
using Dotel2.Models;

namespace Dotel2.Repository.User
{
    public interface IUserRepository
    {
        public Models.User getUserbyRentalId(int uId);
        public bool checkUserMemberShip(Models.User user);
        public Models.User? GetUserByEmailAndPassword(string email, string hashedPassword);

        public Models.User? GetUserByEmail(string email);
        public void RegisterUser(Models.User user);

        void UpdateUserProfile(Models.User user);

        void UpdateUserPassword(string email, string hashedPassword);
    }
}
