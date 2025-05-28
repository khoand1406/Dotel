using Dotel2.Models;
using Dotel2.Repository.Rental;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dotel2.Pages.Admin.Rentals
{
    public class RentalModel : PageModel
    {
        private readonly DotelDBContext _context;
        private readonly IRentalRepository _rentalRepository;
        public RentalModel (DotelDBContext context, IRentalRepository rentalRepository)
        {
            _context = context;
            _rentalRepository = rentalRepository;
        }
        
        [BindProperty]
        public bool ApprovedOnly {  get; set; }

        public List<Rental> Rentals { get; set; }
        public IActionResult OnGet()
        {
            string userJson = HttpContext.Session.GetString("userJson");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/Login/index");
            }
            else
            {
                var user = JsonConvert.DeserializeObject<User>(userJson);
                if (user.RoleId != 1)
                {
                    return RedirectToPage("/Login/index");
                }
            }
            Rentals = _rentalRepository.GetRentals().Where(r => r.Approval).ToList();
            ApprovedOnly = true;
            return Page();
        }
        public void OnPost()
        {
            Rentals = _rentalRepository.GetRentals();
            Rentals = Rentals.Where(r => r.Approval == ApprovedOnly).ToList();
        }
    }
}
