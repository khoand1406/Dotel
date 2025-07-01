using Azure;
using Azure.AI.OpenAI;
using Dotel2.Models;
using Dotel2.Repository.User.ChatAI;
using Microsoft.Extensions.Configuration; 
using Microsoft.Extensions.Logging;
using OpenAI.Chat;
using System.ClientModel;
using System.Runtime.CompilerServices;     

// namespace của bạn
namespace Dotel2.Service.AIChatService
{
    public class OpenAIChatService : IOpenAiChatService
    {
        
        private readonly ChatClient _chatClient;
        private readonly ILogger<OpenAIChatService> _logger;
        private readonly IChatRepository _chatRepository;
        private readonly string _systemPrompt = "You are a helpful assistant.";

        public OpenAIChatService(IConfiguration config, ILogger<OpenAIChatService> logger, IChatRepository chatRepository)
        {
            _logger = logger;

            _chatRepository = chatRepository;
            // Lấy thông tin cấu hình
            var endpoint = new Uri(config["AzureOpenAI:Endpoint"]
                                 ?? throw new ArgumentNullException("AzureOpenAI:Endpoint is not configured."));
            var key = new AzureKeyCredential(config["AzureOpenAI:ApiKey"]
                                           ?? throw new ArgumentNullException("AzureOpenAI:Key is not configured."));
            var deploymentName = config["AzureOpenAI:DeploymentName"] ?? "gpt-35-turbo";

            var azureClient = new AzureOpenAIClient(endpoint, key);
            _chatClient = azureClient.GetChatClient(deploymentName);
        }

        public async Task<string> AskAsync(string prompt, int? userId = null, string? sessionId = null)
        {
            var messages = new List<ChatMessage>
    {
        new SystemChatMessage(_systemPrompt),
        new UserChatMessage(prompt)
    };

            var options = new ChatCompletionOptions
            {
                Temperature = 0.7f,
                MaxOutputTokenCount = 700
            };

            try
            {
                var response = await _chatClient.CompleteChatAsync(messages, options);
                var reply = response.Value.Content[0].Text.Trim();

                // Lưu message người dùng
                await _chatRepository.createChatAsync(userId, new ChatHistory
                {
                    SessionId = sessionId,
                    Message = prompt,
                    Sender = "user",
                        
                });

                await _chatRepository.createChatAsync(userId, new ChatHistory
                {
                    SessionId=sessionId,
                    Message = reply,
                    Sender= "bot"
                });
                return reply;
            }
            catch (RequestFailedException ex)
            {
                _logger.LogError(ex, "Azure OpenAI request failed. Status: {Status}, ErrorCode: {ErrorCode}", ex.Status, ex.ErrorCode);
                return "❌ Xin lỗi, hệ thống đang bận. Vui lòng thử lại sau.";
            }
        }

        public Task<List<ChatHistory>> GetChatHistoryAsync(int? userId, string? sessionId)
        {
            return _chatRepository.GetChatHistoryAsync(userId, sessionId);
        }
    }
}