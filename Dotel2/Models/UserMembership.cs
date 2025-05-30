using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dotel2.Models
{
    public class UserMembership
    {
        [Key]
        public int UserMembershipId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public int MembershipTypeId { get; set; }

        [ForeignKey("MembershipTypeId")]
        public MemberShipType MembershipType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public bool IsActive => DateTime.Now >= StartDate && DateTime.Now <= EndDate;
    }
}
