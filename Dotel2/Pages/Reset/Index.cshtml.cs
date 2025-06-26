using Dotel2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Dotel2.Pages.Reset
{
    public class IndexModel : PageModel
    {
        private readonly DotelDBContext _context;

        public IndexModel(DotelDBContext context)
        {
            _context = context;
        }

        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Password { get; set; }
        [BindProperty] public string RepeatPassword { get; set; }

        public IActionResult OnGet()
        {
            var forgot = HttpContext.Session.GetString("forgot");
            var userVerification = HttpContext.Session.GetString("userVerification");

            if (string.IsNullOrEmpty(userVerification))
            {
                return forgot == "1"
                    ? RedirectToPage("/ForgotPassword/Index")
                    : RedirectToPage("/Login/Index");
            }

            var user = JsonConvert.DeserializeObject<User>(userVerification);
            Email = user?.Email;

            return Page();
        }

        public IActionResult OnPost()
        {
            var forgot = HttpContext.Session.GetString("forgot");
            var userVerification = HttpContext.Session.GetString("userVerification");

            if (string.IsNullOrEmpty(userVerification))
            {
                return forgot == "1"
                    ? RedirectToPage("/ForgotPassword/Index")
                    : RedirectToPage("/Login/Index");
            }

            var userSession = JsonConvert.DeserializeObject<User>(userVerification);
            Email = userSession?.Email;

            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(RepeatPassword))
            {
                TempData["ErrorMessage"] = "Password fields cannot be empty.";
                return Page();
            }

            if (!Password.Equals(RepeatPassword))
            {
                TempData["ErrorMessage"] = "Passwords do not match.";
                return Page();
            }

            var emailExist = _context.Users.FirstOrDefault(s => s.Email.Equals(Email));
            if (emailExist == null)
            {
                return RedirectToPage("/ForgotPassword/Index");
            }

            emailExist.Password = GetHashedPassword(Password);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Change password successfully.";
            HttpContext.Session.Remove("userVerification");
            HttpContext.Session.Remove("forgot");

            return RedirectToPage("/Login/Index");
        }

        private string GetHashedPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }
    }
}
