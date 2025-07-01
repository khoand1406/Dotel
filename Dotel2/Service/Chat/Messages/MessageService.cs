using Dotel2.Models;
using Dotel2.Repository.Message;

namespace Dotel2.Service.Chat.Messages
{
    public class MessageService : iMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public List<Message> getMessagesByConversationId(int conversationId)
        {
            return _messageRepository.getMessagesByConversationId(conversationId);
        }

        public List<Message> getMessagesByUserId(int senderId, int receiver)
        {
            return _messageRepository.getMessagesByUserId(senderId, receiver);
        }

        public int getUnreadMessageCount(int userId)
        {
            return _messageRepository.getUnreadMessageCount(userId);
        }

        public void SendMessage(Message message)
        {
            _messageRepository.SendMessage(message);
        }
    }
}
