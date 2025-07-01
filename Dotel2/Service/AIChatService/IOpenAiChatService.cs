using Dotel2.Models;

namespace Dotel2.Service.AIChatService
{
    public interface IOpenAiChatService
    {
        Task<string> AskAsync(string message, int? userId, string? sessionId);

        Task<List<ChatHistory>> GetChatHistoryAsync(int? userId, string? sessionId);
    }
}
