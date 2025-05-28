using System.ComponentModel.DataAnnotations;
using Dotel2.Models;
using Dotel2.Repository.Rental;
using Dotel2.Repository.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dotel2.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private readonly IUserRepository userRepository;
        private readonly DotelDBContext dbContext;
        public IndexModel( IUserRepository userRepository, DotelDBContext dbContext)
        {
            this.userRepository = userRepository;
            this.dbContext = dbContext;
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

            user = userRepository.getUserbyRentalId(Id);
            return Page();

        }

        public IActionResult OnPost(int Id)
        {
            user = userRepository.getUserbyRentalId(Id);
            user.Email = Email;
            user.Fullname = Fullname;
            user.MainPhoneNumber = MainPhoneNumber;
            user.SecondaryPhoneNumber = SecondaryPhoneNumber;
            dbContext.Update(user);
            dbContext.SaveChanges();
            return RedirectToPage();
        }
    }
}
