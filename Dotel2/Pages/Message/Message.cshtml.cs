using Dotel2.DTOs;
using Dotel2.Models;
using Dotel2.Repository.Conversation;
using Dotel2.Repository.Message;
using Dotel2.Repository.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dotel2.Pages.Message
{
    public class MessageModel : PageModel
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IMessageRepository messageRepository;
        public MessageModel(IConversationRepository conversationRepository, IMessageRepository repository)
        {
            _conversationRepository = conversationRepository;
            this.messageRepository = repository;
        }

        public List<ConversationDTO> Conversations { get; set; } = new();
        public ConversationDTO? ActiveConversation { get; set; }
        public List<Dotel2.Models.Message> Messages { get; set; } = new();
        public User? CurrentUser { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }


        [BindProperty]
        public string MessageContent { get; set; }
        public IActionResult OnGet()
        {
            var userJson = HttpContext.Session.GetString("userJson");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/Login/Index");
            }
            
            CurrentUser = JsonConvert.DeserializeObject<User>(userJson);
            Conversations= _conversationRepository.getConversationsByUserId(CurrentUser.UserId);
            if (TempData["ConversationId"] is int conversationId)
            {
                ActiveConversation= _conversationRepository.GetConversation(conversationId, CurrentUser.UserId);
                Messages= messageRepository.getMessagesByConversationId(conversationId);

            }
            else
            {
                return RedirectToPage("/Error.cshtml");
            }
            

            return Page();


        }
        public IActionResult OnPostSendMessage(int ConversationId)
        {
            var userJson = HttpContext.Session.GetString("userJson");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/Login/Index");
            }

            CurrentUser = JsonConvert.DeserializeObject<User>(userJson);

            if (string.IsNullOrWhiteSpace(MessageContent))
            {
                // Có thể xử lý lỗi nhập trống tại đây
                return RedirectToPage();
            }

            
            var message = new Models.Message
            {
                Content = MessageContent,
                SentAt = DateTime.Now,
                SenderId = CurrentUser.UserId,
                ConversationId = ConversationId,
                
            };
            messageRepository.SendMessage(message);
            ActiveConversation = _conversationRepository.GetConversation(ConversationId, CurrentUser.UserId);
            Messages = messageRepository.getMessagesByConversationId(ConversationId);

            return Page();
        }
    }
}
