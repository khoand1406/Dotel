using Dotel2.DTOs;
using Dotel2.Models;
using Dotel2.Repository.Conversation;
using Dotel2.Repository.Message;
using Dotel2.Repository.User;
using Dotel2.Service.Chat.Conversations;
using Dotel2.Service.Chat.Messages;
using Dotel2.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Dotel2.Pages.Message
{
    public class MessageModel : PageModel
    {
        private readonly IConversationService _conversationService;
        private readonly iMessageService _messageService;
        
        private readonly IHubContext<MessageHub> _hubContext;
        public MessageModel(IConversationService conversationService, iMessageService service, IHubContext<MessageHub> hubContext)
        {
            _conversationService = conversationService;
            _messageService= service;
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
        public async Task<IActionResult> OnGet()
        {
            CurrentUser = getUserInfo();
            Conversations= await _conversationService.GetConversationsByUserId(CurrentUser.UserId);
            var conversationId = HttpContext.Session.GetInt32("ActiveConversationId");
            if (conversationId == null)
            {
                return Page();
            }
            else
            {
                ActiveConversation = await _conversationService.GetConversation(conversationId.Value, CurrentUser.UserId);
                Messages = _messageService.getMessagesByConversationId(conversationId.Value);
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
            _messageService.SendMessage(message);

            // Lấy thông tin user còn lại trong conversation
            var conversation = _conversationService.GetConversation(ConversationId, CurrentUser.UserId).Result;
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
            Conversations = _conversationService.GetConversationsByUserId(CurrentUser.UserId).Result;
            Messages = _messageService.getMessagesByConversationId(ConversationId);

            return RedirectToPage(new { ConversationId });
        }

        public async Task<IActionResult> OnPostOpenConversation()
        {
            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Dictionary<string, int>>(body);
            var conversationId = data["conversationId"];

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Unauthorized();

            Conversations = await _conversationService.GetConversationsByUserId((int)userId);
            ActiveConversation = await _conversationService.GetConversation(conversationId, (int)userId);

            if (ActiveConversation == null)
                return NotFound(new { success = false, message = "Conversation not found." });


            var otherUser = ActiveConversation.User1Id == userId
                ? ActiveConversation.User2
                : ActiveConversation.User1;

            Messages = _messageService.getMessagesByConversationId(conversationId);

            var messages = Messages.Select(m => new
            {
                content = m.Content,
                sentAt = m.SentAt.ToString("HH:mm"),
                isSent = m.SenderId == userId
            });

            return new JsonResult(new
            {
                success = true,
                conversation = new
                {
                    id = ActiveConversation.Id,
                    fullname = otherUser.Fullname
                },
                messages = messages
            });
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

            _messageService.SendMessage(message);

            
            var conv = _conversationService.GetConversation(input.ConversationId, CurrentUser.UserId).Result;
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
