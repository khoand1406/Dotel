using Dotel2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Diagnostics.Contracts;

namespace Dotel2.Pages.Admin.Rentals
{
    public class EditModel : PageModel
    {
        private readonly DotelDBContext _context;
        public EditModel(DotelDBContext context)
        {
            _context = context;
        }
        public class EditRentalModel
        {
            public int RentalId { get; set; }
            public string RentalTitle { get; set; } = null!;
            public string? Description { get; set; }
            public decimal Price { get; set; }
            public decimal? RoomArea { get; set; }
            public int? MaxPeople { get; set; }
            public string? ContactPhoneNumber { get; set; }
            public int? UserId { get; set; }
            public int? ViewNumber { get; set; }
            public bool? Bathroom { get; set; }
            public bool? Kitchen { get; set; }
            public int? BedroomNumber { get; set; }
            public string? Location { get; set; }
            public string? GoogleMap { get; set; }
            public bool Approval { get; set; }
            public bool Status { get; set; }
            public string? Type { get; set; }
        }
        [BindProperty]
        public EditRentalModel EditRental { get; set; }
        public IActionResult OnGet(int id)
        {
            string userJson = HttpContext.Session.GetString("userJson");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/Login/index");
            }
            else
            {
                var user = JsonConvert.DeserializeObject<User>(userJson);
                if (user.RoleId != 1)
                {
                    return RedirectToPage("/Login/index");
                }
            }
            var rental = _context.Rentals.FirstOrDefault(r => r.RentalId == id);
            if (rental == null)
            {
                return NotFound();
            }

            EditRental = new EditRentalModel
            {
                RentalId = rental.RentalId,
                RentalTitle = rental.RentalTitle,
                Description = rental.Description,
                Price = rental.Price,
                RoomArea = rental.RoomArea,
                MaxPeople = rental.MaxPeople,
                ContactPhoneNumber = rental.ContactPhoneNumber,
                UserId = rental.UserId,
                ViewNumber = rental.ViewNumber,
                Bathroom = rental.Bathroom,
                Kitchen = rental.Kitchen,
                BedroomNumber = rental.BedroomNumber,
                Location = rental.Location,
                GoogleMap = rental.GoogleMap,
                Approval = rental.Approval,
                Status = (bool)rental.Status,
                Type = rental.Type
            };

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var rental = _context.Rentals.FirstOrDefault(r => r.RentalId == EditRental.RentalId);
            if (rental == null) return NotFound();

            rental.RentalTitle = EditRental.RentalTitle;
            rental.Description = EditRental.Description;
            rental.Price = (decimal)EditRental.Price;
            rental.RoomArea = EditRental.RoomArea;
            rental.MaxPeople = EditRental.MaxPeople;
            rental.ContactPhoneNumber = EditRental.ContactPhoneNumber;
            rental.UserId = (int)EditRental.UserId;
            rental.ViewNumber = EditRental.ViewNumber;
            rental.Bathroom = EditRental.Bathroom;
            rental.Kitchen = EditRental.Kitchen;
            rental.BedroomNumber = EditRental.BedroomNumber;
            rental.Location = EditRental.Location;
            rental.GoogleMap = EditRental.GoogleMap;
            rental.Approval = EditRental.Approval;
            rental.Status = EditRental.Status;
            rental.Type = EditRental.Type;

            _context.SaveChanges();

            return RedirectToPage("/Admin/Rentals/Index");
        }
        }
    }
