using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Dotel2.Models;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Dotel2.Service.Rental;

namespace Dotel2.Pages.FormRentHome
{
    public class IndexModel : PageModel
    {
        private readonly IRentalService _rentalService;

        public IndexModel(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [BindProperty] public string Title { get; set; }
        [BindProperty] public decimal Price { get; set; }
        [BindProperty] public decimal Area { get; set; }
        [BindProperty] public string Address { get; set; }
        [BindProperty] public string Description { get; set; }
        [BindProperty] public string TypeRoom { get; set; }
        [BindProperty] public string Phone { get; set; }
        [BindProperty] public int NumberP { get; set; }
        [BindProperty] public bool Bathroom { get; set; }
        [BindProperty] public bool Kitchen { get; set; }
        [BindProperty] public int Bedrooms { get; set; }
        [BindProperty] public List<IFormFile> MediaFiles { get; set; }

        public IActionResult OnGet()
        {
            var userJson = HttpContext.Session.GetString("userJson");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/Login/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userJson = HttpContext.Session.GetString("userJson");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/Login/Index");
            }

            var user = JsonConvert.DeserializeObject<User>(userJson);

            if (!_rentalService.IsPhoneValid(Phone))
            {
                TempData["ErrorMessage"] = "Số điện thoại không hợp lệ. Vui lòng nhập đúng định dạng.";
                return Page();
            }

            var rental = new Rental
            {
                UserId = user.UserId,
                RentalTitle = Title,
                Price = Price,
                RoomArea = Area,
                Location = Address,
                Description = Description,
                Type = TypeRoom,
                MaxPeople = NumberP,
                Kitchen = Kitchen,
                Bathroom = Bathroom,
                BedroomNumber = Bedrooms,
                ContactPhoneNumber = Phone,
                Status = true,
                Approval = false,
                ViewNumber = 0,
            };

            await _rentalService.CreateRentalAsync(rental, MediaFiles);
            return RedirectToPage("/Index");
        }
    }



}