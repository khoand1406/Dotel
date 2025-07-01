using Dotel2.Models;
using Dotel2.Service.Admin.Rental;
using Dotel2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Diagnostics.Contracts;

namespace Dotel2.Pages.Admin.Rentals
{
    public class EditModel : PageModel
    {
        private readonly AdminRentalService _rentalService;

        public EditModel(AdminRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [BindProperty]
        public EditRentalModel EditRental { get; set; }
        public IActionResult OnGet(int id)
        {
            var userJson = HttpContext.Session.GetString("userJson");
            if (string.IsNullOrEmpty(userJson)) return RedirectToPage("/Login/Index");

            var user = JsonConvert.DeserializeObject<User>(userJson);
            if (user.RoleId != 1) return RedirectToPage("/Login/Index");

            EditRental = _rentalService.getRentalEdit(id);
            if (EditRental == null) return NotFound();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var success = _rentalService.UpdateRental(EditRental);
            if (!success) return NotFound();

            return RedirectToPage("/Admin/Rentals/Index");
        }
    }
