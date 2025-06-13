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

        public void SendMessage(Models.Message message)
        {
            dbContext.Messages.Add(message);
            dbContext.SaveChanges();
        }
    }
}
