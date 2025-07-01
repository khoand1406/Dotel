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
using Dotel2.Service.User.Register;


namespace Dotel2.Pages.Register
{
    public class IndexModel : PageModel
    {
        private readonly IRegisterService _registerService;

        public IndexModel(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [BindProperty] public string EmailOrPhone { get; set; }
        [BindProperty] public string FullName { get; set; }
        [BindProperty] public string Password { get; set; }
        [BindProperty] public string RepeatPassword { get; set; }

        public void OnGet()
        {
            HttpContext.Session.Clear();
        }

        public IActionResult OnPost()
        {
            var (success, error) = _registerService.Register(EmailOrPhone, Password, RepeatPassword, FullName);

            if (!success)
            {
                TempData["ErrorMessage"] = error;
                return Page();
            }

            TempData["SuccessMessage"] = "Đăng ký thành công.";
            return RedirectToPage("/Login/Index");
        }

        
    }
}
