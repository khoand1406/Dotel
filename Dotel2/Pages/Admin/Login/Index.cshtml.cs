using Dotel2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Dotel2.Pages.Admin.Login
{
    public class IndexModel : PageModel
    {
        private readonly DotelDBContext _context;
        public IndexModel(DotelDBContext context)
        {
            _context = context;
        }
        [BindProperty] public string email { get; set; }
        [BindProperty] public string password { get; set; }

        public void OnGet()
        {
            HttpContext.Session.Clear();
        }
        public IActionResult OnPost()
        {
            if (LoginSuccessful())
            {
                return RedirectToPage("/Admin/Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Tài khoản hoặc mật khẩu không đúng.";
                return Page();
            }
        }

        public bool LoginSuccessful()
        {
            var hashedPassword = GetHashedPassword(password);
            var user = _context.Users.FirstOrDefault(s => s.Email.Equals(email) && s.Password.Equals(hashedPassword));
            if (user == null)
            {
                return false;
            }
            else
            {
                if (user.Status == false) // Deactived
                {
                    TempData["ErrorMessage"] = "Tài khoản đã bị khóa";
                    return false;
                }
                else if (user.RoleId != 1) //Admin
                {
                    TempData["ErrorMessage"] = "Truy cập bị từ chối";
                    return false;
                }

                //set session
                string userJson = JsonConvert.SerializeObject(user);
                /*                Console.WriteLine(userJson);*/
                HttpContext.Session.SetString("userJson", userJson);

                return true;
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
    }
}
