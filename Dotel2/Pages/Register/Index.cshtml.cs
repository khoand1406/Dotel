using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dotel2.Models;
using System.Net.Mail;
using Dotel2.Service;
using Newtonsoft.Json;


namespace Dotel2.Pages.Register
{
    public class IndexModel : PageModel
    {
        private readonly DotelDBContext _context;
        public IndexModel(DotelDBContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            HttpContext.Session.Clear();
        }

        public IActionResult OnPost()
        {
            var input = Request.Form["EmailOrPhone"].ToString().ToLower();
            if (IsValidEmail(input))
            {
                var emailExist = _context.Users.FirstOrDefault(s => s.Email.Equals(input));
                if (emailExist != null)
                {
                    TempData["ErrorMessage"] = "Email đã tồn tại."  ;
                    return Page();
                }
            }
            /*else if (IsValidPhone(input))
            {
                var phoneExist = _context.Users.FirstOrDefault(s => s.MainPhoneNumber.Equals(input));
                if (phoneExist != null)
                {
                    TempData["ErrorMessage"] = "Số điện thoại đã tồn tại.";
                    return Page();
                }
            }*/
            else
            {
                TempData["ErrorMessage"] = "Định dạng email không hợp lệ.";
                return Page();
            }

            if (!Request.Form["Password"].Equals(Request.Form["RepeatPassword"]))
            {
                TempData["ErrorMessage"] = "Mật khẩu không khớp.";
                return Page();
            }

            var hashedPassword = GetHashedPassword(Request.Form["Password"]);

            var newUser = new User
            {
                Fullname = Request.Form["FullName"].ToString().ToLower(),
                Password = hashedPassword,
                RoleId = 2, // Admin = 1, Guest = 2
                Status = true,
                
            };
            //SendMail send = new SendMail();
            //string verificationCode = send.GenerateVerificationCode();
            if (IsValidEmail(input))
            {
                newUser.Email = input;
                newUser.CheckEmail = true;
            }
            /*else if (IsValidPhone(input))
            {
                newUser.MainPhoneNumber = input;
                newUser.CheckPhone = false; // Initially, phone verification is false
                newUser.PhoneVerificationCode = verificationCode;
                newUser.PhoneVerificationCodeExpires = DateTime.Now.AddHours(1);
                send.SendSMSVerification(input, verificationCode);
            }*/

            _context.Users.Add(newUser);
            _context.SaveChanges();


            //string userVerification = JsonConvert.SerializeObject(newUser);
            //HttpContext.Session.SetString("userVerification", userVerification);

            /*TempData["SuccessMessage"] = "Đăng ký thành công. Vui lòng kiểm tra email hoặc điện thoại của bạn để xác nhận.";*/
            TempData["SuccessMessage"] = "Đăng ký thành công.";
            return RedirectToPage("/Login/Index");
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
            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }

        private bool IsValidPhone(string phone)
        {
            var phonePattern = @"^\d{10}$";
            return Regex.IsMatch(phone, phonePattern);
        }
    }
}
