﻿namespace Dotel2.Repository.Message
{
    public interface IMessageRepository
    {
        public List<Models.Message> getMessagesByUserId(int senderId, int receiver);

        public void SendMessage(Models.Message message);

        public List<Models.Message> getMessagesByConversationId(int conversationId);

        
    }
}
