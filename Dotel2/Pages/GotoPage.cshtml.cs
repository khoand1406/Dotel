using Dotel2.Models;
using Dotel2.Repository.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dotel2.Pages
{
    public class GotoPageModel : PageModel
    {
        private IUserRepository userRepository;

        public GotoPageModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public IActionResult OnGet()
        {
            var userJson = HttpContext.Session.GetString("userJson");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/Login/Index");
            }
            var user= JsonConvert.DeserializeObject<User>(userJson);
            var HasMemberShip= userRepository.checkUserMemberShip(user);
            if (HasMemberShip)
            {
                return RedirectToPage("/FormRentHome/Index");
            }
            else
            {
                return RedirectToPage("/Subscription/MemberShip");
            }
        }
    }
}
