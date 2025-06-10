using Dotel2.Models;
using Dotel2.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Dotel2.Pages.Login
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

        public void OnGet()
        {
            HttpContext.Session.Clear();
        }

        public IActionResult OnPost()
        {
            var hashedPassword = GetHashedPassword(Password);
            User user = null;
            SendMail send = new SendMail();
            if (IsValidEmail(Email))
            {
                user = _context.Users.FirstOrDefault(s => s.Email.Equals(Email.ToLower()) && s.Password.Equals(hashedPassword));
                //if (user != null && user.CheckEmail != true)
                //{
                //    TempData["ErrorMessage"] = "Email chưa được xác thực.";

                //    var code = send.GenerateVerificationCode();
                //    send.SendEmailVerification(user.Email, code);

                //    user.EmailVerificationCodeExpires = DateTime.Now.AddHours(1);
                //    user.EmailVerificationCode = code;
                //    _context.SaveChanges();

                //    string userVerification = JsonConvert.SerializeObject(user);
                //    HttpContext.Session.SetString("userVerification", userVerification);

                //    return RedirectToPage("/RequestCode/Index");
                //}
            }
            /*else if (IsValidPhone(Email))
            {
                user = _context.Users.FirstOrDefault(s => s.MainPhoneNumber.Equals(Email) && s.Password.Equals(hashedPassword));
                if (user != null && user.CheckPhone != true)
                {
                    string userVerification = JsonConvert.SerializeObject(user);
                    HttpContext.Session.SetString("userVerification", userVerification);

                    TempData["ErrorMessage"] = "Số điện thoại chưa được xác thực.";
                    return RedirectToPage("/RequestCode/Index");
                }
            }*/
            else
            {
                TempData["ErrorMessage"] = "Định dạng tài khoản không hợp lệ.";
                return Page();
            }

            if (user == null)
            {
                TempData["ErrorMessage"] = "Tài Khoản hoặc mật khẩu không đúng.";
                return Page();
            }
            else
            {
                if (!user.Status) // Deactivated
                {
                    TempData["ErrorMessage"] = "Tài khoản đã bị khóa.";
                    return Page();
                }
                else if (user.RoleId != 2) // Not a guest
                {
                    TempData["ErrorMessage"] = "Truy cập bị từ chối.";
                    return Page();
                }

                // Set session
                string userJson = JsonConvert.SerializeObject(user);
                HttpContext.Session.SetString("userJson", userJson);
                HttpContext.Session.SetInt32("UserId", user.UserId);

                return RedirectToPage("/Index");
            }
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

        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private bool IsValidPhone(string phone)
        {
            var phonePattern = @"^\d{10}$";
            return Regex.IsMatch(phone, phonePattern);
        }
    }
}
