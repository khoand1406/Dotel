namespace Dotel2.Service.Chat.Messages
{
    public interface iMessageService
    {
        public List<Models.Message> getMessagesByUserId(int senderId, int receiver);

        public void SendMessage(Models.Message message);

        public List<Models.Message> getMessagesByConversationId(int conversationId);

        public int getUnreadMessageCount(int userId);

    }
}
