using Dotel2.Models;
using Dotel2.Repository.Rental;
using Dotel2.Service.Rental;
using EXE_Dotel.Repository.Rental;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dotel2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRentalService _service;
        
        public IndexModel(IRentalService service)
        {
           _service= service;
        }
        public bool IsLoggedIn { get; private set; }
        public List<Rental> rentals { get; private set; }
        public Dictionary<int, List<RentalListImage>> images { get; private set; }


        public string? SessionValue { get; private set; }


        [BindProperty(SupportsGet = true)]
        public string Location { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Type { get; set; }

        [BindProperty(SupportsGet = true)]
        public Decimal? MinArea { get; set; }
        [BindProperty(SupportsGet = true)]
        public Decimal? MaxArea { get; set; }
        [BindProperty(SupportsGet = true)]
        public Decimal? MinPrice { get; set; }
        [BindProperty(SupportsGet = true)]
        public Decimal? MaxPrice { get; set; }

        public User userSession { get; set; }

        public List<Rental> FilteredRenter { get; set; }
        public void OnGet()
        {
            var userJson = HttpContext.Session.GetString("userJson");
            if (!string.IsNullOrEmpty(userJson))
            {
                userSession = JsonConvert.DeserializeObject<User>(userJson);
            }

            var pagesize = 6;

            IsLoggedIn = !string.IsNullOrEmpty(userJson);

            rentals = _service.getRentalWithImage(pagesize);
            
            
            
            foreach (var r in rentals)
            {
                SessionValue = HttpContext.Session.GetString("UserSession");
                var curListImg = _service.getRentalWithListImages(r.RentalId);
                

            }
            ViewData["CntPost"] = rentals.Count;

        }
        public IActionResult OnPostIncrementViewCount(int rentalId)
        {
            var rental = _service.GetRental(rentalId);
           


            if (rental != null)
            {
                _service.getViewCountIncrease(rental);
                
                return RedirectToPage("RentHomeDetails", new { id = rentalId });
            }
            return NotFound();
        }


        public IActionResult OnPostIndex()
        {

            return RedirectToPage("RentHome",new { Location, Type, MaxArea, MinArea, MinPrice, MaxPrice});
        }
    }


}
