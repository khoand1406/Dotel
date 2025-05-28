using Dotel2.Models;
using Dotel2.Repository.Rental;
using EXE_Dotel.Repository.Rental;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Dotel2.Pages
{
    public class RentHomeModel : PageModel
    {
        private readonly IRentalRepository repository;

        public RentHomeModel(IRentalRepository repo)
        {
            repository = repo;

        }
        public List<Rental> Rentals { get; set; }
        public int Total { get; private set; }

        public int PageSize { get; private set; } = 9;

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public int TotalPages { get; private set; }


        //Thanh
        public string? userSessionTime { get; set; }
        //

        [BindProperty(SupportsGet =true)]

        public string? Location { get; set; }

        [BindProperty(SupportsGet =true)]

        public string? Type { get; set; }

        [BindProperty(SupportsGet = true)]
        public Decimal? MinArea { get; set; }
        [BindProperty(SupportsGet = true)]
        public Decimal? MaxArea { get; set; }
        [BindProperty(SupportsGet = true)]
        public Decimal? MinPrice { get; set; }
        [BindProperty(SupportsGet = true)]
        public Decimal? MaxPrice { get; set; }

        public List<Rental> FilteredRenter { get; private set; }

        public void OnGet()
        {
            //Thanh
            var userSession = HttpContext.Session.GetString("UserSession");
            userSessionTime = userSession;
            //
            Console.WriteLine($"Current Page: {CurrentPage}");

            FilteredRenter = repository.getFilterRentalPaging(Location,Type, MaxArea, MinArea, MinPrice, MaxPrice);
            Total = repository.getListRentalsCount(FilteredRenter);
            TotalPages = (int)Math.Ceiling(Total / (double)PageSize);
            FilteredRenter = repository.getRentersPaging(FilteredRenter, CurrentPage, PageSize);


        }

        public IActionResult OnPostIncrementViewCount(int rentalId)
        {
            var rental = repository.GetRental(rentalId);
            if (rental != null)
            {
                repository.getViewCountIncrease(rental);
                
                return RedirectToPage("RentHomeDetails", new { id = rentalId });
            }
            return NotFound();
        }
        public IActionResult OnPostIndex()
        {
            return RedirectToPage(new { Location, Type, MaxArea, MinArea, MinPrice, MaxPrice, CurrentPage, PageSize });
        }
    }
}
