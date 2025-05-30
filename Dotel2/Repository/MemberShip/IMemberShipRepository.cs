using Dotel2.Models;

namespace Dotel2.Repository.MemberShip
{
    public interface IMemberShipRepository
    {
        public List<MemberShipType> GetMemberShips();

        public UserMembership GetUserMemberShip(Models.User user);
    }
}
