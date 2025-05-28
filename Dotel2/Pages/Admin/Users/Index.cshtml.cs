using Dotel2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Dotel2.Pages.Admin.Users
{
    public class UserModel : PageModel
    {
        private readonly DotelDBContext _context;
        public UserModel(DotelDBContext context)
        {
            _context = context;
        }
        public List<User> Users { get; set; }
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
            Users = _context.Users.Include(r => r.Role).ToList();
            return Page();
        }
    }
}
