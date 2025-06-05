using Dotel2.Models;
using Dotel2.Repository.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dotel2.Pages.Message
{
    public class MessageModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public MessageModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<Conversations> Conversations { get; set; } = new();
        public Conversations? ActiveConversation { get; set; }
        public List<Dotel2.Models.Message> Messages { get; set; } = new();
        public User? CurrentUser { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }
        public IActionResult OnGet()
        {
            var userJson = HttpContext.Session.GetString("userJson");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/Login/Index");
            }
            
            CurrentUser = JsonConvert.DeserializeObject<User>(userJson);
            Conversations= _userRepository.getConversationsByUserId(CurrentUser.UserId);

            return Page();


        }
    }
}
