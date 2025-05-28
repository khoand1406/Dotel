using Dotel2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient.Server;
using Newtonsoft.Json;
using System;

namespace Dotel2.Pages.RequestCode
{
    public class IndexModel : PageModel
    {
        private readonly DotelDBContext _context;
        public IndexModel(DotelDBContext context)
        {
            _context = context;
        }
        public User UserVerification { get; set; }

        [BindProperty] public string Code { get; set; }

        public string emailSession { get; set; }
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

            var users = JsonConvert.DeserializeObject<User>(userVerification);
            UserVerification = users;
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
            }
            var users = JsonConvert.DeserializeObject<User>(userVerification);
            UserVerification = _context.Users.FirstOrDefault(s => s.Email == users.Email);

            if (UserVerification == null)
            {
                return RedirectToPage("/Login/index");
            }

            if (string.IsNullOrEmpty(Code))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập mã xác thực.";
                return Page();
            }
            else
            {
                if (Code == UserVerification.EmailVerificationCode && UserVerification.EmailVerificationCodeExpires > DateTime.Now)
                {
                    UserVerification.CheckEmail = true;
                    UserVerification.EmailVerificationCodeExpires = null;
                    UserVerification.EmailVerificationCode = null;
                    _context.SaveChanges();
                    if (forgot != null && forgot == "1")
                    {
                        return RedirectToPage("/Reset/Index");
                    }
                    TempData["SuccessMessage"] = "Xác thực email thành công.";
                    return RedirectToPage("/Login/Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Mã xác thực không hợp lệ hoặc đã hết hạn.";
                    return Page();
                }
            }
        }
    }
}
