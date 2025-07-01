
using Dotel2.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotel2.Repository.User.ChatAI
{
    public class ChatRepository : IChatRepository
    {
        private readonly DotelDBContext _dbContext;

        public ChatRepository(DotelDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> createChatAsync(int? userId, ChatHistory chat)
        {
            try
            {
                chat.UserId = userId;
                chat.Timestamp = DateTime.UtcNow;
                _dbContext.ChatHistories.Add(chat);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"[Chat] Error: {ex.Message}");
                return false;
            }
        }

        public async Task<List<ChatHistory>> GetChatHistoryAsync(int? userId, string? sessionId)
        {
            return await _dbContext.ChatHistories
        .Where(c => (userId != null && c.UserId == userId) || (sessionId != null && c.SessionId == sessionId))
        .OrderBy(c => c.Timestamp)
        .ToListAsync();
        }

        public async Task<bool> hasChatHistory(int userId)
        {
            return await _dbContext.ChatHistories.FirstOrDefaultAsync(c => c.UserId == userId)==null;
        }
    }
}
