using Dotel2.Models;
using Dotel2.Service;
using Dotel2.Service.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace Dotel2.Pages.ForgotPassword
{
    public class IndexModel : PageModel
    {
        private readonly DotelDBContext _context;
        private readonly ISendMailService _sendMailService;
        public IndexModel(DotelDBContext context, ISendMailService sendMailService)
        {
            _context = context;
            _sendMailService = sendMailService;
        }
        [BindProperty] public string username { get; set; }

        private string GenerateVerificationCode()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[4];
                rng.GetBytes(randomBytes);
                return BitConverter.ToString(randomBytes).Replace("-", "");
            }
        }
        public void OnGet()
        {
            var userVerification = HttpContext.Session.GetString("userVerification");
            if (!string.IsNullOrEmpty(userVerification))
            {
                HttpContext.Session.Remove("userVerification");
            }
        }
        public async Task<IActionResult> OnPost()
        {
            var user = _context.Users.FirstOrDefault(s => s.Email.Equals(username));
            if (user == null)
            {
                TempData["ErrorMessage"] = "Email not exist.";
                return Page();
            }
            else
            {
                
                var code = GenerateVerificationCode();

                var subject = "Dotel - Mã xác thực đặt lại mật khẩu";
                var body = $"<p>Chào {user.Fullname},</p>" +
                           $"<p>Mã xác thực của bạn là: <strong>{code}</strong></p>" +
                           $"<p>Mã có hiệu lực trong 1 giờ.</p>";

                var success = await _sendMailService.SendEmailAsync(user.Email, subject, body);

                if (!success)
                {
                    TempData["ErrorMessage"] = "Không thể gửi email. Vui lòng thử lại sau.";
                    return Page();
                }

                // Lưu vào DB
                user.EmailVerificationCode = code;
                user.EmailVerificationCodeExpires = DateTime.Now.AddHours(1);
                _context.SaveChanges();

                // Lưu vào Session
                string userVerification = JsonConvert.SerializeObject(user);
                HttpContext.Session.SetString("userVerification", userVerification);
                HttpContext.Session.SetString("forgot", "1");

                return RedirectToPage("/RequestCode/Index");
            }
        }
    }
}
