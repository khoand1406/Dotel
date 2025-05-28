using Dotel2.Models;
using Dotel2.Repository.Rental;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dotel2.Pages.Profile
{
    public class PostHistoryModel : PageModel
    {
        private readonly DotelDBContext _context;
        private readonly IRentalRepository _rentalRepository;
        public List<Rental> Rentals { get; set; }
        public PostHistoryModel(DotelDBContext context, IRentalRepository rentalRepository)
        {
            _context = context;
            _rentalRepository = rentalRepository;
        }
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
            Rentals = _rentalRepository.GetRentals().Where(r => r.UserId == Id).ToList();
            return Page();
        }
    }
}
