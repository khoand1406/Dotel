using Dotel2.Models;
using Dotel2.Service;
using Dotel2.Service.User.Login;
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
        private readonly ILoginService _loginService;

        public IndexModel(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Password { get; set; }

        public void OnGet()
        {
            HttpContext.Session.Clear();
        }

        public IActionResult OnPost()
        {
            var user = _loginService.AuthenticateUser(Email, Password, out string error);

            if (!string.IsNullOrEmpty(error))
            {
                TempData["ErrorMessage"] = error;
                return Page();
            }
            if(user == null)
            {
                TempData["ErrorMessage"] = error;
                return Page();
            }

            // Set session
            HttpContext.Session.SetString("userJson", JsonConvert.SerializeObject(user));
            HttpContext.Session.SetInt32("UserId", user.UserId);

            return RedirectToPage("/Index");
        }
    }
}