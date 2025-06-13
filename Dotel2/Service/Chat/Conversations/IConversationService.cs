using Dotel2.DTOs;

namespace Dotel2.Service.Chat.Conversations
{
    public interface IConversationService
    {
        void CreateNewConversation(Models.Conversations conversation);
        ConversationDTO GetConversationByUserId(int userIdFrom, int userIdTo);
        ConversationDTO GetConversation(int conversationId, int currentUserId);
        List<ConversationDTO> GetConversationsByUserId(int userId);
        ConversationDTO GetOrCreateConversation(int currUserId, int targetUserId);
    }
}
