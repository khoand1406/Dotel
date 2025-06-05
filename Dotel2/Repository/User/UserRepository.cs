
using Dotel2.Models;

namespace Dotel2.Repository.User
{
    public class UserRepository : IUserRepository
    {
        DotelDBContext context;
        public UserRepository(DotelDBContext dBContext) {
            this.context = dBContext;
        }

        public bool checkUserMemberShip(Models.User user)
        {
            return context.UserMemberships
                .Any(ship => ship.UserId == user.UserId && ship.EndDate> DateTime.UtcNow );
        }

        public List<Conversations> getConversationsByUserId(int userIdFrom)
        {
            return context.Conversations.Where
                (conv=> conv.User1Id == userIdFrom)
                .OrderBy(conv=> conv.CreatedAt)
                .ToList();
        }

        public List<Message> getMessagesByConversationId(int conversationId)
        {
            throw new NotImplementedException();
        }

        public List<Message> getMessagesByUserId(int senderId, int receiver)
        {
            throw new NotImplementedException();
        }

        public Models.User getUserbyRentalId(int uId)
        {
            return context.Users.FirstOrDefault(user => user.UserId == uId);
        }

        public void SendMessage(Message message, int senderId, int receiver)
        {
            throw new NotImplementedException();
        }
    }
}
