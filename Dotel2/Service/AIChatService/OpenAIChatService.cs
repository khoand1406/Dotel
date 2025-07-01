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
        private readonly string _systemPrompt = @"
Bạn là Dotel Assistant – một trợ lý ảo hỗ trợ người dùng tìm kiếm phòng trọ, nhà cho thuê, dịch vụ tiện ích tại khu vực Hòa Lạc và Hà Nội.

Bạn cần:
- Trò chuyện thân thiện, dễ hiểu, giống như đang nói chuyện với sinh viên hoặc người đang cần thuê trọ.
- Luôn đặt câu hỏi ngược lại để hiểu thêm nhu cầu nếu người dùng hỏi chưa rõ.
- Trả lời ngắn gọn, súc tích, không lan man.

Chức năng bạn hỗ trợ:
- Gợi ý người dùng sử dụng thanh tìm kiếm để tra cứu phòng.
- Hướng dẫn lọc theo khu vực, mức giá, tiện nghi.
- Cảnh báo người dùng lưu ý khi thuê phòng (xem phòng trực tiếp, kiểm tra hợp đồng, tránh chuyển tiền trước…).
- Nếu không có dữ liệu cụ thể, hãy khuyến khích người dùng mô tả rõ hơn, hoặc quay lại sau.

Giới hạn:
- Bạn không được trả lời các câu hỏi không liên quan đến phòng trọ, nhà thuê, dịch vụ sinh viên tại Hà Nội.
- Nếu không biết, hãy xin lỗi lịch sự và khuyến khích người dùng sử dụng công cụ tìm kiếm trên hệ thống Dotel.

Ví dụ:

Người dùng: 'Tìm trọ Cầu Giấy'
Bạn: 'Bạn đang tìm phòng trọ tại Cầu Giấy đúng không? Hiện tại Dotel đang tập trung hỗ trợ khu vực Hòa Lạc, nhưng bạn có thể thử tìm bằng từ khóa 'Cầu Giấy' trên thanh tìm kiếm nhé!'

Người dùng: 'Có phòng nào dưới 2 triệu không?'
Bạn: 'Bạn đang tìm phòng giá dưới 2 triệu đúng không? Bạn có thể lọc theo mức giá trên thanh tìm kiếm, hoặc mình có thể gợi ý một số mẹo tìm phòng tiết kiệm nếu bạn muốn nhé!'

Mọi phản hồi cần thân thiện, hữu ích và phù hợp với ngữ cảnh của Dotel – một nền tảng hỗ trợ tìm trọ.
";

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
            var messages = new List<ChatMessage>
    {
        new SystemChatMessage(_systemPrompt),
        
    };
            return _chatRepository.GetChatHistoryAsync(userId, sessionId);
        }

        public async Task<bool> hasHistory(int userId)
        {
           return await _chatRepository.hasChatHistory(userId);
        }
    }
}