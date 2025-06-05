using Dotel2.Models;
using Dotel2.Repository.User;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dotel2.Controllers
{
    [ApiController]
    [Route("/api/messages/conversations")]
    public class ConversationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public ConversationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public IActionResult getConversations()
        {
            var userJson = HttpContext.Session.GetString("userJson");
            if(string.IsNullOrEmpty(userJson)) {
                return Unauthorized("Người dùng chưa đăng nhập");
            }

            var user = JsonConvert.DeserializeObject<User>(userJson);
            return Ok(user);

        }
    }
}
