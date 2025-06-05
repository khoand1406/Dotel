using Dotel2.Models;
using Dotel2.Repository.User;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;

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
            var userJson = HttpContext.Session.GetString("userJson");
            if (string.IsNullOrEmpty(userJson))
            {
                return Unauthorized(new { error = "Chưa đăng nhập" });
            }

            try
            {
                var user = JsonConvert.DeserializeObject<User>(userJson);
            }
            catch
            {
                return BadRequest(new {error= "Sai format user"});
            }
            return Ok(new { success = true });
        }
    }

    public class MessageRequestDto
    {
        public int RecipientId { get; set; }
        public string Content { get; set; }
    }
}
