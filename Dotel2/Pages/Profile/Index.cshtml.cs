using System.ComponentModel.DataAnnotations;
using Dotel2.Models;
using Dotel2.Repository.Rental;
using Dotel2.Repository.User;
using Dotel2.Service.User.Profile;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dotel2.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private readonly IUserProfileService _userProfileService;
        public IndexModel( IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }
        [BindProperty(SupportsGet = true)]
        public User user { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Fullname { get; set; }
        [BindProperty(SupportsGet = true)]
        public string MainPhoneNumber { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SecondaryPhoneNumber { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }
        public IActionResult OnGet(int Id)
        {
            var userSession = HttpContext.Session.GetString("userJson");
            if (userSession == null)
            {
                return RedirectToPage("/Login/index");
            }

            if (Id != JsonConvert.DeserializeObject<User>(userSession).UserId)
            {
                return RedirectToPage("/Login/index");
            }

            user = _userProfileService.getUserById(Id);
            return Page();

        }

        public IActionResult OnPost(int Id)
        {
            string error;
            var success = _userProfileService.UpdateUserProfile(Id, Fullname, MainPhoneNumber, SecondaryPhoneNumber, Email, out error);

            if (!success)
            {
                TempData["ErrorMessage"] = error;
                return Page();
            }

            TempData["SuccessMessage"] = "Cập nhật thông tin thành công.";
            return RedirectToPage(new { Id= Id});
        }
    }
}
