using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotel2.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RentalId { get; set; }

        [ForeignKey(nameof(RentalId))]
        public Rental Rental { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating phải từ 1 đến 5 sao.")]
        public int Rating { get; set; } // 1 to 5 stars

        [Required(ErrorMessage = "Hãy nhập nhận xét.")]
        [StringLength(1000, ErrorMessage = "Nhận xét không được dài quá 1000 ký tự.")]
        public string Comment { get; set; }

        [Display(Name = "Ngày đánh giá")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
