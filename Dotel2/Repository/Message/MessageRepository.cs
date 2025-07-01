using Dotel2.Models;

namespace Dotel2.Repository.Message
{
    public class MessageRepository:IMessageRepository
    {
        private readonly DotelDBContext dbContext;
        public MessageRepository(DotelDBContext context)
        {
            this.dbContext = context;
        }
        public List<Models.Message> getMessagesByConversationId(int conversationId)
        {
            return dbContext.Messages.Where(msg => msg.ConversationId == conversationId).ToList();
        }

        public List<Models.Message> getMessagesByUserId(int senderId, int receiver)
        {
            throw new NotImplementedException();
        }

        public int getUnreadMessageCount(int userId)
        {
            var conversations = dbContext.Conversations
        .Where(c => c.User1Id == userId || c.User2Id == userId)
        .Select(c => c.ConversationId)
        .ToList();

            
            var readMap = dbContext.UserConversationReads
                .Where(rc => rc.UserId == userId)
                .ToDictionary(rc => rc.ConversationId, rc => rc.LastReadAt);

            
            var unreadCount = dbContext.Messages
                .Where(m => conversations.Contains(m.ConversationId) &&
                            m.SenderId != userId &&
                            (!readMap.ContainsKey(m.ConversationId) || m.SentAt > readMap[m.ConversationId]))
                .Count();

            return unreadCount;
        }

        public void SendMessage(Models.Message message)
        {
            dbContext.Messages.Add(message);
            dbContext.SaveChanges();
        }
    }
}
