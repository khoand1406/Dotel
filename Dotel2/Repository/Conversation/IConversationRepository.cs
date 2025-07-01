using Dotel2.DTOs;
using Dotel2.Models;

namespace Dotel2.Repository.Conversation
{
    public interface IConversationRepository
    {
        void createNewConvesation(Conversations conversation);
        ConversationDTO getConversationByUserId(int userIdFrom, int userIdTo);
        Task<ConversationDTO> GetConversation(int conversationId, int currentUserId);
        Task<List<ConversationDTO>> getConversationsByUserId(int userId);
        Task<ConversationDTO> getOrCreateConversation(int currUserId, int targetUserId);

        public Task UpdateReadTime(int conversationId, int userId);
    }
}
