
using Dotel2.Models;

namespace Dotel2.Repository.User
{
    public class UserRepository : IUserRepository
    {
        DotelDBContext context;
        public UserRepository(DotelDBContext dBContext) {
            this.context = dBContext;
        }

        public Models.User getUserbyRentalId(int uId)
        {
            return context.Users.FirstOrDefault(user => user.UserId == uId);
        }
    }
}
