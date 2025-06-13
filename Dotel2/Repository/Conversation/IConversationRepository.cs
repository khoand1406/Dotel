using Dotel2.DTOs;
using Dotel2.Models;

namespace Dotel2.Repository.Conversation
{
    public interface IConversationRepository
    {
        void createNewConvesation(Conversations conversation);
        ConversationDTO getConversationByUserId(int userIdFrom, int userIdTo);
        ConversationDTO GetConversation(int conversationId, int currentUserId);
        List<ConversationDTO> getConversationsByUserId(int userId);
        ConversationDTO getOrCreateConversation(int currUserId, int targetUserId);
    }
}
