using Dotel2.Service.AIChatService;
using Microsoft.AspNetCore.Mvc;
using OpenAI.Chat;
using System.Text.Json;

namespace Dotel2.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        private readonly IOpenAiChatService _chatService;

        public ChatController(IOpenAiChatService chatService)
        {
            _chatService = chatService;
        }
        public class ChatRequest
        {
            public string message { get; set; }
        }

        [HttpGet("history")]
        public async Task<IActionResult> getChatHistory()
        {
            var userSessionJson = HttpContext.Session.GetString("userJson");
            int? userId = null;

            if (!string.IsNullOrEmpty(userSessionJson))
            {
                try
                {
                    var userObj = JsonDocument.Parse(userSessionJson);
                    userId = userObj.RootElement.GetProperty("UserId").GetInt32();
                }
                catch(Exception ex) {
                    return BadRequest(ex.Message);
                }
            }

            var sessionId = HttpContext.Session.GetString("ChatSessionId");

            var history = await _chatService.GetChatHistoryAsync(userId, sessionId);

            return Ok(history.Select(h => new
            {
                timestamp= h.Timestamp,
                message= h.Message,
                sender= h.Sender
            }));
        }



        [HttpPost("ask")]
        public async Task<IActionResult> AskAsync([FromBody] ChatRequest chatRequest)
        {
            
            if (string.IsNullOrWhiteSpace(chatRequest.message))
                return BadRequest("Tin nhắn rỗng.");

            var userSession = HttpContext.Session.GetString("userJson");
            int? userId = null;

            if (!string.IsNullOrEmpty(userSession))
            {
                try
                {
                    var userObj = JsonDocument.Parse(userSession);
                    userId = userObj.RootElement.GetProperty("UserId").GetInt32();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Không thể parse session user: " + ex.Message);
                }
            }

            if (HttpContext.Session.GetString("ChatSessionId") == null)
            {
                HttpContext.Session.SetString("ChatSessionId", Guid.NewGuid().ToString());
            }
            var messages= new List<ChatMessage>();
            var sessionId = HttpContext.Session.GetString("ChatSessionId");

            try
            {
                var reply = await _chatService.AskAsync(chatRequest.message, userId,sessionId ); 
                return Ok(reply);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi AI: {ex.Message}");
                return StatusCode(500, "Lỗi hệ thống, thử lại sau.");
            }
        }
    }
}
