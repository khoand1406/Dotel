
using Dotel2.DTOs;
using Dotel2.Models;
using Microsoft.EntityFrameworkCore;

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

        public ConversationDTO GetConversation(int conversationId, int currentUserId)
        {
            var conversation = context.Conversations
         .Include(c => c.User1)
         .Include(c => c.User2)
         .Include(c => c.Messages)
         .FirstOrDefault(c => c.ConversationId == conversationId);

            if (conversation == null) return null;

            
            var lastReadAt = context.UserConversationReads
                .Where(r => r.ConversationId == conversationId && r.UserId == currentUserId)
                .Select(r => (DateTime?)r.LastReadAt)
                .FirstOrDefault();

            
            int unreadCount = conversation.Messages
                .Where(m => m.SenderId != currentUserId &&
                            (lastReadAt == null || m.SentAt > lastReadAt))
                .Count();

            return new ConversationDTO
            {
                Id = conversation.ConversationId,
                User1Id = conversation.User1Id,
                User2Id = conversation.User2Id,
                User1 = conversation.User1,
                User2 = conversation.User2,
                OtherUser = (conversation.User1Id == currentUserId) ? conversation.User2 : conversation.User1,
                LastMessage = conversation.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault(),
                UnreadCount = unreadCount
            };
        }

        public Conversations getConversationByUserId(int userIdFrom, int userIdTo)
        {
            return context.Conversations.FirstOrDefault
                (conv => conv.User1Id == userIdFrom && conv.User2Id == userIdTo);
                
        }

        public List<ConversationDTO> getConversationsByUserId(int userId)
        {
            var conversations= context.Conversations.Include(conv => conv.User1)
                .Include(conv => conv.User2)
                .Include(conv => conv.Messages)
                .Select(conv => new ConversationDTO
                {
                    Id = conv.ConversationId,
                    User1Id = conv.User1Id,
                    User2Id = conv.User2Id,
                    User1 = conv.User1,
                    User2 = conv.User2,
                    LastMessage = conv.Messages.OrderByDescending(msg => msg.SentAt).FirstOrDefault(),
                    OtherUser = conv.User1Id == userId ? conv.User2 : conv.User1,
                    UnreadCount = conv.Messages.Where(msg => msg.SenderId != userId)
                                              .Count(msg => msg.SentAt > context.UserConversationReads
                                                                  .Where(r => r.ConversationId == conv.ConversationId && r.UserId == userId)
                                                                  .Select(r => r.LastReadAt).FirstOrDefault())
                }).Where(conv => conv.User1Id == userId || conv.User2Id == userId)
                .ToList();
            return conversations;

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
