using Dotel2.Models;

namespace Dotel2.Repository.User
{
    public interface IUserRepository
    {
        public Models.User getUserbyRentalId(int uId);
    }
}
