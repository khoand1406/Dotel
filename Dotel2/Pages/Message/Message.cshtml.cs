using Dotel2.Models;
using Dotel2.Repository.Message;
using Dotel2.Repository.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dotel2.Pages.Message
{
    public class MessageModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository messageRepository;
        public MessageModel(IUserRepository userRepository, IMessageRepository repository)
        {
            _userRepository = userRepository;
            this.messageRepository = repository;
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
            if (TempData["ConversationId"] is int conversationId)
            {
                ActiveConversation= _userRepository.GetConversation(conversationId);
                Messages= messageRepository.getMessagesByConversationId(conversationId);

            }
            else
            {
                return RedirectToPage("/Error.cshtml");
            }
            

            return Page();


        }
    }
}
