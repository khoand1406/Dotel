using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotel2.Models
{
    public class Conversations
    {
        [Key]
        public int ConversationId { get; set; }

        [Required]
        public int User1Id { get; set; }

        [Required]
        public int User2Id { get; set; }

        [ForeignKey("User1Id")]
        public User User1 { get; set; }

        [ForeignKey("User2Id")]
        public User User2 { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }= DateTime.Now;

        
        public virtual ICollection<Message> Messages { get; set; }
    }
}
