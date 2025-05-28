using Dotel2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dotel2.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public ActionResult OnGet()
        {
            string userJson = HttpContext.Session.GetString("userJson");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/Login/index");
            }
            else
            {
                var user = JsonConvert.DeserializeObject<User>(userJson);
                if (user.RoleId == 1)
                {
                    return Page();
                }
                else
                {
                    return RedirectToPage("/Login/index");
                }
            }
        }
    }
}
