using Dotel2.Models;
using Dotel2.Repository.MemberShip;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dotel2.Pages.Subscription
{
    public class MemberShipModel : PageModel
    {
        private IMemberShipRepository memberShipRepository;
        public MemberShipModel(IMemberShipRepository memberShipRepository) { 
            this.memberShipRepository = memberShipRepository;
        }

        public List<MemberShipType> memberShips { get; set; }
        public void OnGet()
        {
            memberShips= memberShipRepository.GetMemberShips();
        }
    }
}
