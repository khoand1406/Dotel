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
        public IActionResult OnGet()
        {
            var forgot = HttpContext.Session.GetString("forgot");

            var userVerification = HttpContext.Session.GetString("userVerification");
            if (string.IsNullOrEmpty(userVerification))
            {
                if (forgot != null && forgot == "1")
                {
                    return RedirectToPage("/ForgotPassword/index");
                }

                return RedirectToPage("/Login/index");
            }
            return Page();
        }

        public IActionResult OnPost()
        {

            var forgot = HttpContext.Session.GetString("forgot");

            var userVerification = HttpContext.Session.GetString("userVerification");
            if (string.IsNullOrEmpty(userVerification))
            {
                if (forgot != null && forgot == "1")
                {
                    return RedirectToPage("/ForgotPassword/index");
                }

                return RedirectToPage("/Login/index");
            }else
            {
                var user = JsonConvert.DeserializeObject<User>(userVerification);
                Email = user.Email;
            }

            var emailExist = _context.Users.FirstOrDefault(s => s.Email.Equals(Email));

            if (emailExist == null)
            {
                return RedirectToPage("/forgotpassword/index");
            }

            if (!Request.Form["Password"].Equals(Request.Form["RepeatPassword"]))
            {
                TempData["ErrorMessage"] = "Passwords do not match.";
                return Page();
            }

            var hashedPassword = GetHashedPassword(Request.Form["Password"]);
            emailExist.Password = hashedPassword;
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Chagne password successfully.";
            return RedirectToPage("/login/index");
        }
        private string GetHashedPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
