using Dotel2.Models;

namespace Dotel2.DTOs
{
    public class ConversationDTO
    {
        public int Id { get; set; }

        public int User1Id { get; set; }
        public int User2Id { get; set; }

        public User User1 { get; set; }
        public User User2 { get; set; }

        public User? OtherUser { get; set; }

        public Dotel2.Models.Message? LastMessage { get; set; }

        public int UnreadCount { get; set; }
    }
}
