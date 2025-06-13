using Dotel2.DTOs;
using Dotel2.Models;
using Dotel2.Repository.Rental;
using Dotel2.Repository.Reviews;
using Dotel2.Repository.User;
using Dotel2.Service.Chat;
using Dotel2.Service.Chat.Conversations;
using Dotel2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace Dotel2.Pages
{
    public class RentHomeDetailsModel : PageModel
    {
        private readonly IRentalRepository repository;
        
        private readonly IReviewRepository reviewRepository;

        private readonly IChatService _chatService;
        public RentHomeDetailsModel(IChatService chatService, IRentalRepository repo,IReviewRepository reviewRepo)
        {
            repository = repo;
            
            this.reviewRepository = reviewRepo;

            _chatService = chatService;
        }

        public Rental Rental { get; set; }
        
        public string? userSessionTime { get; set; }
        //
        public User User { get; set; }

        public List<ReviewViewModel> Reviews { get; set; }

        [BindProperty]
        public ReviewInputModel NewReview { get; set; }

        public IActionResult OnGet(int Id)
        {
            //Thanh
            var userSession = HttpContext.Session.GetString("userJson");
            userSessionTime = userSession;
            //
            Rental = repository.GetRental(Id);

            if (!string.IsNullOrEmpty(Rental.Description))
            {
                Rental.Description = FormatDescription(Rental);
            }
            
            Reviews= reviewRepository.GetReviews(Id);

           
            return Page();
        }

        public string FormatDescription(Rental rental) { 
        
                string description= rental.Description.ToString();
                string title = rental.RentalTitle.ToString();

            foreach(var pattern in _formatPatterns)
            {
                if (!string.IsNullOrEmpty(description))
                {
                    var replacement = pattern.Value.Replace("{RentalTitle}",title);
                    description = Regex.Replace(description, pattern.Key, replacement);
                }
            }
            if (description.Contains("<ul>") && !description.Contains("</ul>"))
            {
                description += "</ul>";
            }
            if (description.Contains("<p>") && !description.Contains("</p>"))
            {
                description += "</p>";
            }

            return description;


        }

        private static readonly Dictionary<String, String> _formatPatterns = new Dictionary<string, string> 
            {
                { @"(?<=\n|^)- ", "<li>" }, // Bullet points
                { @"(\r?\n|$)", "</li>" }, // End of list item
                { @"(?<=\n|^)Phí dịch vụ:", "<h3>Phí dịch vụ:</h3><ul>" }, // Section headers
                { @"(?<=\n|^)Phòng được trang bị đầy đủ nội thất và thiết bị:", "</ul><h3>Phòng được trang bị đầy đủ nội thất và thiết bị:</h3><p>" },
                { @"(?<=\n|^)Điểm nổi bật của {RentalTitle}:", "</p><h3>Điểm nổi bật của {RentalTitle}:</h3><ul>" },

                { @"(?<=\n|^)HOLA GATE không chỉ đầy đủ tiện nghi mà còn có vị trí đẹp, không gian thoáng đãng và nhiều dịch vụ hỗ trợ tiện lợi. Hãy liên hệ ngay để trải nghiệm!", 
                "</ul><p>HOLA GATE không chỉ đầy đủ tiện nghi mà còn có vị trí đẹp, không gian thoáng đãng và nhiều dịch vụ hỗ trợ tiện lợi. Hãy liên hệ ngay để trải nghiệm!</p>" }
            };

        public IActionResult OnPost()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId==null) return RedirectToPage("/Login/Index");
            int id = userId.Value;

            var review = new Review {
                Rating = NewReview.Rating,
                Comment = NewReview.Comment,
                RentalId = NewReview.RentalId,
                UserId = id,
                CreatedAt = DateTime.Now

            };
            reviewRepository.CreateReview(review);
            return RedirectToPage(new { Id = NewReview.RentalId });

        }
        
        public IActionResult OnPostStartConversation(int targetUserId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null) return RedirectToPage("/Login/Index");
            var conversationDto = _chatService.GetOrCreateConversation(userId.Value, targetUserId);

            TempData["ConversationId"] = conversationDto.Id;
            return new JsonResult(new { success = true });
        }
    }
    
}
