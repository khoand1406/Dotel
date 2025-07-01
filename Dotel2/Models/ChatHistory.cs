using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dotel2.Models
{
    public class ChatHistory
    {
        [Key]
        public int Id { get; set; }

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Message { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Sender { get; set; } = "user"; // "user" hoặc "bot"

        [MaxLength(100)]
        public string? SessionId { get; set; } // Với người chưa đăng nhập

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
