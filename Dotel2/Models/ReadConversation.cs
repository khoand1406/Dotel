using Microsoft.VisualBasic;

namespace Dotel2.Models
{
    public class ReadConversation
    {
        public int ConversationId { get; set; }
        public int UserId { get; set; }

        public DateTime LastReadAt { get; set; } = DateTime.UtcNow;

        
        public Conversations Conversation { get; set; }= new Conversations();

        public User User { get; set; } = new User();

    }
}
