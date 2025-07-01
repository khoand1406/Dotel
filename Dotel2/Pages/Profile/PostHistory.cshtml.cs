using Dotel2.Models;
using Dotel2.Repository.Rental;
using Dotel2.Service.Rental;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dotel2.Pages.Profile
{
    public class PostHistoryModel : PageModel
    {
        
        private readonly IRentalService _service;
        public List<Rental> Rentals { get; set; }
        public PostHistoryModel(IRentalService service)
        {
            _service = service;
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
            Rentals = _service.GetRentals().Where(r => r.UserId == Id).ToList();
            return Page();
        }
    }
}
