using Dotel2.Models;
using Dotel2.Service.Admin.Rental;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dotel2.Pages.Admin.Rentals
{
    public class DeleteModel : PageModel
    {
        private readonly AdminRentalService _rentalService;

        public DeleteModel(AdminRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        public class DeleteRentalModel
        {
            public int id;
        }

        public DeleteRentalModel deleteRental { get; set; }

        public IActionResult OnGet(int id)
        {
            string userJson = HttpContext.Session.GetString("userJson");
            if (string.IsNullOrEmpty(userJson)) return RedirectToPage("/Login/index");

            var user = JsonConvert.DeserializeObject<User>(userJson);
            if (user.RoleId != 1) return RedirectToPage("/Login/index");

            var rental = _rentalService.getRentalEdit(id);
            if (rental == null) return NotFound();

            deleteRental = new DeleteRentalModel { id = rental.RentalId };
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid) return Page();

            var success = _rentalService.deleteRental(id);
            if (!success) return NotFound();

            return RedirectToPage("./Index");
        }
    }
}
