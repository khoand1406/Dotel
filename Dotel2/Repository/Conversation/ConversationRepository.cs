using Dotel2.DTOs;
using Dotel2.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotel2.Repository.Conversation
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly DotelDBContext context;

        public ConversationRepository(DotelDBContext context)
        {
            this.context = context;
        }
        public void createNewConvesation(Conversations conversations)
        {
            context.Conversations.Add(conversations);
            context.SaveChanges();
        }

        public ConversationDTO GetConversation(int conversationId, int currentUserId)
        {
            var conversation = context.Conversations
         .Include(c => c.User1)
         .Include(c => c.User2)
         .Include(c => c.Messages)
         .FirstOrDefault(c => c.ConversationId == conversationId);

            if (conversation == null) return null;


            var lastReadAt = context.UserConversationReads
                .Where(r => r.ConversationId == conversationId && r.UserId == currentUserId)
                .Select(r => (DateTime?)r.LastReadAt)
                .FirstOrDefault();


            int unreadCount = conversation.Messages
                .Where(m => m.SenderId != currentUserId &&
                            (lastReadAt == null || m.SentAt > lastReadAt))
                .Count();

            return new ConversationDTO
            {
                Id = conversation.ConversationId,
                User1Id = conversation.User1Id,
                User2Id = conversation.User2Id,
                User1 = conversation.User1,
                User2 = conversation.User2,
                OtherUser = (conversation.User1Id == currentUserId) ? conversation.User2 : conversation.User1,
                LastMessage = conversation.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault(),
                UnreadCount = unreadCount
            };
        }

        public ConversationDTO getConversationByUserId(int currentUserId, int otherUserId)
        {
            var conversation = context.Conversations
        .Include(c => c.User1)
        .Include(c => c.User2)
        .Include(c => c.Messages)
        .FirstOrDefault(c =>
            (c.User1Id == currentUserId && c.User2Id == otherUserId) ||
            (c.User1Id == otherUserId && c.User2Id == currentUserId));

            if (conversation == null) return null;

            var lastReadAt = context.UserConversationReads
                .Where(r => r.ConversationId == conversation.ConversationId && r.UserId == currentUserId)
                .Select(r => (DateTime?)r.LastReadAt)
                .FirstOrDefault();

            int unreadCount = conversation.Messages
                .Where(m => m.SenderId != currentUserId &&
                            (lastReadAt == null || m.SentAt > lastReadAt))
                .Count();

            return new ConversationDTO
            {
                Id = conversation.ConversationId,
                User1Id = conversation.User1Id,
                User2Id = conversation.User2Id,
                User1 = conversation.User1,
                User2 = conversation.User2,
                OtherUser = (conversation.User1Id == currentUserId) ? conversation.User2 : conversation.User1,
                LastMessage = conversation.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault(),
                UnreadCount = unreadCount
            };

        }

        public List<ConversationDTO> getConversationsByUserId(int userId)
        {
            var conversations = context.Conversations.Include(conv => conv.User1)
                .Include(conv => conv.User2)
                .Include(conv => conv.Messages)
                .Select(conv => new ConversationDTO
                {
                    Id = conv.ConversationId,
                    User1Id = conv.User1Id,
                    User2Id = conv.User2Id,
                    User1 = conv.User1,
                    User2 = conv.User2,
                    LastMessage = conv.Messages.OrderByDescending(msg => msg.SentAt).FirstOrDefault(),
                    OtherUser = conv.User1Id == userId ? conv.User2 : conv.User1,
                    UnreadCount = conv.Messages.Where(msg => msg.SenderId != userId)
                                              .Count(msg => msg.SentAt > context.UserConversationReads
                                                                  .Where(r => r.ConversationId == conv.ConversationId && r.UserId == userId)
                                                                  .Select(r => r.LastReadAt).FirstOrDefault())
                }).Where(conv => conv.User1Id == userId || conv.User2Id == userId)
                .ToList();
            return conversations;

        }

        public ConversationDTO getOrCreateConversation(int currUserId, int targetUserId)
        {
            var conversation = context.Conversations
            .Include(c => c.User1)
            .Include(c => c.User2)
            .Include(c => c.Messages)
            .FirstOrDefault(c =>
                (c.User1Id == currUserId && c.User2Id == targetUserId) ||
                (c.User1Id == targetUserId && c.User2Id == currUserId));

            if (conversation == null)
            {
                conversation = new Conversations
                {
                    User1Id = currUserId,
                    User2Id = targetUserId,
                    CreatedAt = DateTime.Now
                };

                context.Conversations.Add(conversation);
                context.SaveChanges();

                // Load navigation properties
                conversation = context.Conversations
                    .Include(c => c.User1)
                    .Include(c => c.User2)
                    .Include(c => c.Messages)
                    .FirstOrDefault(c => c.ConversationId == conversation.ConversationId);
            }

            return new ConversationDTO
            {
                Id = conversation.ConversationId,
                User1Id = conversation.User1Id,
                User2Id = conversation.User2Id,
                User1 = conversation.User1,
                User2 = conversation.User2,
                OtherUser = (conversation.User1Id == currUserId) ? conversation.User2 : conversation.User1,
                LastMessage = conversation.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault()
            };
        }
    }
}
