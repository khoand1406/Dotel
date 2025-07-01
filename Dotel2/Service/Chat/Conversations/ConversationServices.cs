using Dotel2.DTOs;
using Dotel2.Repository.Conversation;

namespace Dotel2.Service.Chat.Conversations
{
    public class ConversationServices : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;

        public ConversationServices(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }
        public void CreateNewConversation(Models.Conversations conversation)
        {
            _conversationRepository.createNewConvesation(conversation);
        }

        public ConversationDTO GetConversationByUserId(int userIdFrom, int userIdTo)
        {
            return _conversationRepository.getConversationByUserId(userIdFrom, userIdTo);
        }

        public async Task<ConversationDTO> GetConversation(int conversationId, int currentUserId)
        {
            return  await _conversationRepository.GetConversation(conversationId, currentUserId);
        }

        public Task<List<ConversationDTO>> GetConversationsByUserId(int userId)
        {
            return _conversationRepository.getConversationsByUserId(userId);
        }

        public async Task<ConversationDTO> GetOrCreateConversation(int currUserId, int targetUserId)
        {
            return await _conversationRepository.getOrCreateConversation(currUserId, targetUserId);
        }

        public Task UpdateReadTime(int conversationId, int userId)
        {
            return _conversationRepository.UpdateReadTime(conversationId, userId);
        }
    }
}

