using Dotel2.DTOs;
using Dotel2.Models;

namespace Dotel2.Service.Chat
{
    public interface IChatService
    {
        ConversationDTO GetOrCreateConversation(int currentUserId, int targetUserId);
    }
}
