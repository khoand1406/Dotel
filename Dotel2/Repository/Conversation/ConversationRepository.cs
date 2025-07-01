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

        public async Task<ConversationDTO> GetConversation(int conversationId, int currentUserId)
        {
            var conversation = await context.Conversations
         .Include(c => c.User1)
         .Include(c => c.User2)
         .Include(c => c.Messages)
         .FirstOrDefaultAsync(c => c.ConversationId == conversationId);

            if (conversation == null) return null;


            var lastReadAt = await context.UserConversationReads
                .Where(r => r.ConversationId == conversationId && r.UserId == currentUserId)
                .Select(r => (DateTime?)r.LastReadAt)
                .FirstOrDefaultAsync();


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

        public async Task<List<ConversationDTO>> getConversationsByUserId(int userId)
        {
            var conversations = await context.Conversations
        .Include(c => c.User1)
        .Include(c => c.User2)
        .Include(c => c.Messages)
        .Where(c => c.User1Id == userId || c.User2Id == userId)
        .ToListAsync();

            
            var readTimes = await context.UserConversationReads
                .Where(r => r.UserId == userId)
                .ToDictionaryAsync(r => r.ConversationId, r => r.LastReadAt);

            
            var result = conversations.Select(conv =>
            {
                readTimes.TryGetValue(conv.ConversationId, out DateTime lastReadAt);

                int unreadCount = conv.Messages
                    .Where(m => m.SenderId != userId && m.SentAt > lastReadAt)
                    .Count();

                return new ConversationDTO
                {
                    Id = conv.ConversationId,
                    User1Id = conv.User1Id,
                    User2Id = conv.User2Id,
                    User1 = conv.User1,
                    User2 = conv.User2,
                    OtherUser = conv.User1Id == userId ? conv.User2 : conv.User1,
                    LastMessage = conv.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault(),
                    UnreadCount = unreadCount
                };
            }).ToList();

            return result;

        }

        public async Task<ConversationDTO> getOrCreateConversation(int currUserId, int targetUserId)
        {
            var conversation = await context.Conversations
            .Include(c => c.User1)
            .Include(c => c.User2)
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(c =>
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
                await context.SaveChangesAsync();

                Console.WriteLine("Taoooooooooooooooooooooooooo");
                // Load navigation properties
                conversation = await context.Conversations
                    .Include(c => c.User1)
                    .Include(c => c.User2)
                    .Include(c => c.Messages)
                    .FirstOrDefaultAsync(c => c.ConversationId == conversation.ConversationId);
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

        public async Task UpdateReadTime(int conversationId, int userId)
        {
            
            var existing = await context.UserConversationReads
        .FirstOrDefaultAsync(rc => rc.ConversationId == conversationId && rc.UserId == userId);

            if (existing == null)
            {
                context.UserConversationReads.Add(new ReadConversation
                {
                    ConversationId = conversationId,
                    UserId = userId,
                    LastReadAt = DateTime.UtcNow
                });
            }
            else
            {
                existing.LastReadAt = DateTime.UtcNow;
                context.UserConversationReads.Update(existing);
            }

            await context.SaveChangesAsync();
        }
    }
}
