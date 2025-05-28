using Dotel2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dotel2.Pages.Admin.Users
{
    public class DeleteModel : PageModel
    {
        private readonly DotelDBContext _context;
        public DeleteModel(DotelDBContext context)
        {
            _context = context;
        }
        public class DeleteUser
        {
            public int id;
        }
        public DeleteUser deleteUser;
        public User User {  get; set; }
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
            User = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (User == null) return NotFound();
            deleteUser = new DeleteUser
            {
                id = User.UserId
            };
            return Page();
        }
        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid) return Page();
            User = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (User == null) return NotFound();
            _context.Remove(User);
            _context.SaveChanges();
            return RedirectToPage("/Admin/Users/Index");
        }
    }
}
