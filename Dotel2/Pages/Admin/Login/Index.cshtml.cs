using Dotel2.Models;
using Dotel2.Service.Admin.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Dotel2.Pages.Admin.Login
{
    public class IndexModel : PageModel
    {
        private readonly IAdminAuthService _authService;

        public IndexModel(IAdminAuthService authService)
        {
            _authService = authService;
        }

        [BindProperty] public string email { get; set; }
        [BindProperty] public string password { get; set; }

        public void OnGet()
        {
            HttpContext.Session.Clear();
        }

        public IActionResult OnPost()
        {
            if (_authService.Authenticate(email, password, out var user, out string message))
            {
                string userJson = JsonConvert.SerializeObject(user);
                HttpContext.Session.SetString("userJson", userJson);
                return RedirectToPage("/Admin/Index");
            }

            TempData["ErrorMessage"] = message;
            return Page();
        }
    }
}

