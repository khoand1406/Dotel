using Dotel2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dotel2.Pages.Admin.Rentals
{
    public class ViewImagesModel : PageModel
    {
        private readonly DotelDBContext _context;
        public ViewImagesModel(DotelDBContext context)
        {
            _context = context;
        }
        public List <RentalListImage> Images { get; set; }
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
            Images = _context.RentalListImages.Where(i => i.RentalId == id).ToList();
            if (Images == null) return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteImageAsync(int imageId)
        {
            var image = await _context.RentalListImages.FindAsync(imageId);
            if (image == null) return NotFound();
            _context.RentalListImages.Remove(image);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
