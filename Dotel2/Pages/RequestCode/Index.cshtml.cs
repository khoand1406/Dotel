using Dotel2.Models;
using Dotel2.Repository.User;
using Dotel2.Service.User.EmailVerfification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient.Server;
using Newtonsoft.Json;
using System;

namespace Dotel2.Pages.RequestCode
{
    public class IndexModel : PageModel
    {
        private readonly IEmailVerificationService _verificationService;
        private readonly IUserRepository _userRepository;

        public IndexModel(IUserRepository userRepository, IEmailVerificationService verificationService)
        {
            _userRepository = userRepository;
            _verificationService = verificationService;
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
            var userVerificationJson = HttpContext.Session.GetString("userVerification");

            if (string.IsNullOrEmpty(userVerificationJson))
            {
                return forgot == "1"
                    ? RedirectToPage("/ForgotPassword/Index")
                    : RedirectToPage("/Login/Index");
            }

            var sessionUser = JsonConvert.DeserializeObject<User>(userVerificationJson);
            var user = _userRepository.GetUserByEmail(sessionUser.Email);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Người dùng không tồn tại.";
                return RedirectToPage("/Login/Index");
            }

            if (!_verificationService.ValidateCode(user, Code, out var errorMsg))
            {
                TempData["ErrorMessage"] = errorMsg;
                return Page();
            }

            TempData["SuccessMessage"] = "Xác thực email thành công.";
            return forgot == "1" ? RedirectToPage("/Reset/Index") : RedirectToPage("/Login/Index");
        }
    }
}
