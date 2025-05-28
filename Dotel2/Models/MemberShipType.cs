using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotel2.Models
{
    public class MemberShipType
    {
        [Key]
        public int MembershipTypeId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(0, 999999999)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 3650)]
        public int DurationInDays { get; set; } // Thời hạn tính theo ngày

        public ICollection<UserMembership> UserMemberships { get; set; }

    }
}
