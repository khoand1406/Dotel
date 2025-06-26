using Dotel2.DTOs;
using Dotel2.Models;
using Dotel2.Repository.Conversation;
using Dotel2.Repository.Message;
using Dotel2.Repository.User;
using Dotel2.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Dotel2.Pages.Message
{
    public class MessageModel : PageModel
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IMessageRepository messageRepository;
        private readonly IHubContext<MessageHub> _hubContext;
        public MessageModel(IConversationRepository conversationRepository, IMessageRepository repository, IHubContext<MessageHub> hubContext)
        {
            _conversationRepository = conversationRepository;
            this.messageRepository = repository;
            _hubContext = hubContext;
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
            CurrentUser = getUserInfo();
            Conversations= _conversationRepository.getConversationsByUserId(CurrentUser.UserId);
            var conversationId = HttpContext.Session.GetInt32("ActiveConversationId");
            if (conversationId == null)
            {
                return RedirectToPage("/Error");
            }
            else
            {
                ActiveConversation = _conversationRepository.GetConversation(conversationId.Value, CurrentUser.UserId);
                Messages = messageRepository.getMessagesByConversationId(conversationId.Value);
            }

            return Page();

        }
        public async Task<IActionResult> OnPostSendMessage(int ConversationId)
        {
            CurrentUser = getUserInfo();

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

            // Lưu tin nhắn vào DB
            messageRepository.SendMessage(message);

            // Lấy thông tin user còn lại trong conversation
            var conversation = _conversationRepository.GetConversation(ConversationId, CurrentUser.UserId);
            var receiverId = (conversation.User1Id == CurrentUser.UserId)
                                ? conversation.User2Id
                                : conversation.User1Id;

            // Gửi tin nhắn đến người nhận qua SignalR nếu họ đang online
            var connectionId = MessageHub.GetConnectionId(receiverId);
            if (!string.IsNullOrEmpty(connectionId))
            {
                await _hubContext.Clients.Client(connectionId)
                    .SendAsync("ReceiveMessage", CurrentUser.UserId, MessageContent, ConversationId);
            }

            // Load lại data để render giao diện
            ActiveConversation = conversation;
            Conversations = _conversationRepository.getConversationsByUserId(CurrentUser.UserId);
            Messages = messageRepository.getMessagesByConversationId(ConversationId);

            return RedirectToPage(new { ConversationId });
        }

        public IActionResult OnPostOpenConversation(int ConversationId)
        {
            CurrentUser = getUserInfo();

            if (CurrentUser == null)
            {
                return RedirectToPage("/Login/Index");
            }

            Conversations = _conversationRepository.getConversationsByUserId(CurrentUser.UserId);
            ActiveConversation = _conversationRepository.GetConversation(ConversationId, CurrentUser.UserId);
            Messages = messageRepository.getMessagesByConversationId(ConversationId);

            return Page();
        }

        private User? getUserInfo()
        {
            var userJson = HttpContext.Session.GetString("userJson");
            if (string.IsNullOrEmpty(userJson))
            {
                RedirectToPage("/Login/Index");
            }
            return JsonConvert.DeserializeObject<User>(userJson);
        }

        
        public async Task<JsonResult> OnPostAjaxSendMessage([FromBody] MessageDTO input)
        {
            CurrentUser = getUserInfo();

            var message = new Models.Message
            {
                Content = input.MessageContent,
                SentAt = DateTime.Now,
                SenderId = CurrentUser.UserId,
                ConversationId = input.ConversationId,
            };

            messageRepository.SendMessage(message);

            
            var conv = _conversationRepository.GetConversation(input.ConversationId, CurrentUser.UserId);
            var receiverId = conv.User1Id == CurrentUser.UserId ? conv.User2Id : conv.User1Id;

            var connId = MessageHub.GetConnectionId(receiverId);
            if (!string.IsNullOrEmpty(connId))
            {
                await _hubContext.Clients.Client(connId).SendAsync("ReceiveMessage", CurrentUser.UserId, input.MessageContent, input.ConversationId);
            }

            return new JsonResult(new
            {
                success = true,
                sentAt = DateTime.Now.ToString("HH:mm")
            });
        }

    }
}
