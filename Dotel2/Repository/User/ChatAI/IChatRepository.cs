using Dotel2.Models;

namespace Dotel2.Repository.User.ChatAI
{
    public interface IChatRepository
    {
        public bool hasChatHistory(int userId);

        public Task<bool> createChatAsync(int? userId, ChatHistory chat);

        public Task<List<ChatHistory>> GetChatHistoryAsync(int? userId, string? sessionId);
    }
}
