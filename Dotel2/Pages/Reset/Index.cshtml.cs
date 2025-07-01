using Dotel2.Models;
using Dotel2.Service.User.ResetPassword;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Dotel2.Pages.Reset
{
    public class IndexModel : PageModel
    {
        private readonly IResetPasswordService _resetService;

        public IndexModel(IResetPasswordService resetService)
        {
            _resetService = resetService;
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
            var userVerificationJson = HttpContext.Session.GetString("userVerification");

            if (string.IsNullOrEmpty(userVerificationJson))
            {
                return forgot == "1"
                    ? RedirectToPage("/ForgotPassword/Index")
                    : RedirectToPage("/Login/Index");
            }

            var sessionUser = JsonConvert.DeserializeObject<User>(userVerificationJson);
            Email = sessionUser?.Email;

            var success = _resetService.ResetPassword(Email, Password, RepeatPassword, out string msg);

            if (!success)
            {
                TempData["ErrorMessage"] = msg;
                return Page();
            }

            TempData["SuccessMessage"] = "Đổi mật khẩu thành công.";
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
