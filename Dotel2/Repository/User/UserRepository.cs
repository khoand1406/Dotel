
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

        public void createNewConvesation(Conversations conversations)
        {
            context.Conversations.Add(conversations);
            context.SaveChanges();
        }

        public Conversations GetConversation(int conversationId)
        {
            return context.Conversations.FirstOrDefault(conv => conv.ConversationId == conversationId);
        }

        public Conversations getConversationByUserId(int userIdFrom, int userIdTo)
        {
            return context.Conversations.FirstOrDefault
                (conv => conv.User1Id == userIdFrom && conv.User2Id == userIdTo);
                
        }

        public List<Conversations> getConversationsByUserId(int userId)
        {
            return context.Conversations.Where(conv=> conv.User1Id==userId)
                .OrderBy(conv=> conv.CreatedAt)
                .ToList();
        }

        public Models.User getUserbyRentalId(int uId)
        {
            return context.Users.FirstOrDefault(user => user.UserId == uId);
        }

        public void SendMessage(Models.Message message, int senderId, int receiver)
        {
            throw new NotImplementedException();
        }
    }
}
