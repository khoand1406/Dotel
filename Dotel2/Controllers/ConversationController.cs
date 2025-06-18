using Dotel2.Models;
using Dotel2.Repository.User;
using Dotel2.Service.Chat.Conversations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dotel2.Controllers
{
    [ApiController]
    [Route("/api/conversations/get-all")]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _conversationService;

        public ConversationController(IConversationService conversationService)
        {
            _conversationService= conversationService;
        }
        [HttpPost]
        public IActionResult getConversations([FromBody] int targetUserid)
        {
            var userJson = HttpContext.Session.GetString("userJson");
            if(string.IsNullOrEmpty(userJson)) {
                return Unauthorized("Người dùng chưa đăng nhập");
            }
            try
            {
                var user = JsonConvert.DeserializeObject<User>(userJson);
                var conversations = _conversationService.GetOrCreateConversation(user.UserId, targetUserid);
                return Ok(conversations);
            }
            catch(Exception ex)
            {
                return BadRequest("Sai user format! Vui long dang nhap lai");
            }
            
        }
    }
}
