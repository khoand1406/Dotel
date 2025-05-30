using Dotel2.Models;
using Dotel2.Repository.User;

namespace Dotel2.Repository.MemberShip
{
    public class MemberShipRepository : IMemberShipRepository
    {
        
        private readonly IUserRepository _userRepository;
        private readonly DotelDBContext _dotelDBContext;
        public MemberShipRepository(IUserRepository userRepository, DotelDBContext dotelDBContext)
        {
            _userRepository = userRepository;
            _dotelDBContext = dotelDBContext;
        }

        public List<MemberShipType> GetMemberShips()
        {
            return _dotelDBContext.MembershipTypes.ToList();
        }

        public UserMembership GetUserMemberShip(Models.User user)
        {
            return _dotelDBContext.UserMemberships.FirstOrDefault(member=> member.UserId== user.UserId);
        }
    }
}
