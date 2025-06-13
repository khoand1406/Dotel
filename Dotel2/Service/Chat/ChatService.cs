using Dotel2.DTOs;
using Dotel2.Models;
using Dotel2.Repository.Conversation;
using Dotel2.Repository.User;

namespace Dotel2.Service.Chat
{
    public class ChatService : IChatService
    {
        private IConversationRepository _conversationRepository;

        public ChatService(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }
        public ConversationDTO GetOrCreateConversation(int currentUserId, int targetUserId)
        {
            if (currentUserId == targetUserId)
                throw new InvalidOperationException("Cannot chat with yourself");

            var conversation = _conversationRepository.getOrCreateConversation(currentUserId, targetUserId);

            return conversation;
        }
    }
}
