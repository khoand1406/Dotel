using Dotel2.Repository.User;
using Microsoft.AspNetCore.Mvc;

namespace Dotel2.Controllers
{
    [ApiController]
    [Route("/api/messages")]
    public class MessageController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public MessageController(IUserRepository userRepository) { 
            _userRepository = userRepository;
        }

        [HttpPost]
        public IActionResult sendMessage([FromBody] MessageRequestDto messageRequestDto)
        {
            return Ok(new { success = true });
        }
    }

    public class MessageRequestDto
    {
        public int RecipientId { get; set; }
        public string Content { get; set; }
    }
}
