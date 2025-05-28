using Dotel2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dotel2.Pages.Admin.Rentals
{
    public class DeleteModel : PageModel
    {
        public readonly DotelDBContext _context;
        public DeleteModel(DotelDBContext context)
        {
            _context = context;
        }
        public class DeleteRentalModel
        {
            public int id;
        }
        public DeleteRentalModel deleteRental { get; set; }
        public IActionResult OnGet(int id)
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
            var rental = _context.Rentals.FirstOrDefault(r => r.RentalId == id);
            if (rental == null) return NotFound();
            deleteRental = new DeleteRentalModel
            {
                id = rental.RentalId
            };
            return Page();
        }
        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid) return Page();
            var rental = _context.Rentals.FirstOrDefault(r => r.RentalId == id);
            if (rental == null) return NotFound();
            _context.Rentals.Remove(rental);
            _context.SaveChanges();
            return RedirectToPage("./Index");
        }
    }
}
