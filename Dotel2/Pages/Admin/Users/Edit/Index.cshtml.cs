using Dotel2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dotel2.Pages.Admin.Users.Edit
{
    public class IndexModel : PageModel
    {
        private readonly DotelDBContext _context;
        public IndexModel(DotelDBContext context)
        {
            _context = context;
        }
        public class EditUserViewModel
        {
            public int UserId { get; set; }
            public string Fullname { get; set; }
            public string Email { get; set; }
            public string MainPhoneNumber { get; set; }
            public string? SecondaryPhoneNumber { get; set; }
            public bool Status { get; set; }
            public int RoleId { get; set; }
        }
        [BindProperty]
        public EditUserViewModel EditUser { get; set; }
        public User User { get; set; }
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
            if (User == null) { return NotFound(); }
            EditUser = new EditUserViewModel
            {
                UserId = User.UserId,
                Fullname = User.Fullname,
                Email = User.Email,
                MainPhoneNumber = User.MainPhoneNumber,
                SecondaryPhoneNumber = User.SecondaryPhoneNumber,
                Status = User.Status,
                RoleId = User.RoleId
            };
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) { return Page(); }
            var user = _context.Users.FirstOrDefault(u => u.UserId == EditUser.UserId);
            if (user == null) { return NotFound(); };
            user.Fullname = EditUser.Fullname;
            user.Email = EditUser.Email;
            user.SecondaryPhoneNumber = EditUser.SecondaryPhoneNumber;
            user.Status = EditUser.Status;
            user.RoleId = EditUser.RoleId;
            _context.SaveChanges();
            return RedirectToPage("/Admin/Users/Index");
        }
    }
}
