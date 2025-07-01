using Dotel2.DTOs;

namespace Dotel2.Service.Chat.Conversations
{
    public interface IConversationService
    {
        void CreateNewConversation(Models.Conversations conversation);
        ConversationDTO GetConversationByUserId(int userIdFrom, int userIdTo);
        Task<ConversationDTO> GetConversation(int conversationId, int currentUserId);
        Task<List<ConversationDTO>> GetConversationsByUserId(int userId);
        Task<ConversationDTO> GetOrCreateConversation(int currUserId, int targetUserId);

        public Task UpdateReadTime(int conversationId, int userId);
    }
}
