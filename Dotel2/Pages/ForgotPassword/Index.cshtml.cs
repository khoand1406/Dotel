using Dotel2.Models;
using Dotel2.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dotel2.Pages.ForgotPassword
{
    public class IndexModel : PageModel
    {
        private readonly DotelDBContext _context;
        public IndexModel(DotelDBContext context)
        {
            _context = context;
        }
        [BindProperty] public string username { get; set; }
        public void OnGet()
        {
            var userVerification = HttpContext.Session.GetString("userVerification");
            if (!string.IsNullOrEmpty(userVerification))
            {
                HttpContext.Session.Remove("userVerification");
            }
        }
        public IActionResult OnPost()
        {
            var user = _context.Users.FirstOrDefault(s => s.Email.Equals(username));
            if (user == null)
            {
                TempData["ErrorMessage"] = "Email not exist.";
                return Page();
            }
            else
            {
                SendMail send = new SendMail();
                var code = send.GenerateVerificationCode();
                send.SendEmailVerification(user.Email, code);

                user.EmailVerificationCode = code;
                user.EmailVerificationCodeExpires = DateTime.Now.AddHours(1);
                _context.SaveChanges();

                string userVerification = JsonConvert.SerializeObject(user);
                HttpContext.Session.SetString("userVerification", userVerification);
                HttpContext.Session.SetString("forgot", "1");
                return RedirectToPage("/requestcode/index");
            }
        }
    }
}
