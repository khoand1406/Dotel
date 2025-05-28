using Dotel2.Models;
using Dotel2.Repository.Rental;
using EXE_Dotel.Repository.Rental;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dotel2.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;
        private readonly IRentalRepository rentalRepository;
        private readonly DotelDBContext _context;
        public IndexModel(IRentalRepository repository, DotelDBContext context)
        {
            rentalRepository = repository;
            _context = context;
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

            rentals = rentalRepository.getRentalWithImage(pagesize);
            
            
            
            foreach (var r in rentals)
            {
                SessionValue = HttpContext.Session.GetString("UserSession");
                var curListImg = rentalRepository.getRentalWithListImages(r.RentalId);
                //images[r.RentalId] = curListImg;

            }
            ViewData["CntPost"] = rentals.Count;
/*            ViewData["CntUser"] = _context.Users.ToList().Count;*/
        }
        public IActionResult OnPostIncrementViewCount(int rentalId)
        {
            var rental = rentalRepository.GetRental(rentalId);
           


            if (rental != null)
            {
                rentalRepository.getViewCountIncrease(rental);
                
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
