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

namespace Dotel2.Pages.FormRentHome
{
    public class IndexModel : PageModel
    {
        private readonly DotelDBContext _context;
        private readonly string _uploadDirectory = "wwwroot/uploads";

        public IndexModel(DotelDBContext context)
        {
            _context = context;

            if (!Directory.Exists(_uploadDirectory))
            {
                Directory.CreateDirectory(_uploadDirectory);
            }
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

        public ActionResult OnGet()
        {
            var userJson = HttpContext.Session.GetString("userJson");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/Login/index");
            }
            return Page();
        }

        public ActionResult OnPost()
        {
            var userJson = HttpContext.Session.GetString("userJson");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/login");
            }
            var user = JsonConvert.DeserializeObject<User>(userJson);


            // Validate số điện thoại theo Regular Expression
            if (!Regex.IsMatch(Phone, @"^0\d{9}$"))
            {
                TempData["ErrorMessage"] = "Số điện thoại không hợp lệ. Vui lòng nhập đúng định dạng.";
                return Page();
            }

            Console.WriteLine(Kitchen);
            Console.WriteLine(Bathroom);
            var rental = new Rental
            {
                UserId = user.UserId,
                RentalTitle = Title,
                Price = Price,
                RoomArea = Area,
                Description = FormatDescriptionForSave(Description),
                ContactPhoneNumber = Phone.ToString(),
                Location = Address,
                Type = TypeRoom,
                MaxPeople = NumberP,
                Kitchen = Kitchen,
                Bathroom = Bathroom,
                BedroomNumber = Bedrooms,
                Status = true,
                Approval = false,
                ViewNumber = 0,
            };
            _context.Rentals.Add(rental);
            _context.SaveChanges();


            var rentalId = rental.RentalId;

            var imagePaths = new List<string>();

            var rentalDirectory = Path.Combine(_uploadDirectory, rentalId.ToString());
            var imageDirectory = Path.Combine(rentalDirectory, "img");

            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            int imageCount = 1;

            foreach (var file in MediaFiles)
            {
                if (file.Length > 0 && file.ContentType.StartsWith("image/"))
                {
                    string newFileName = $"{imageCount++}{Path.GetExtension(file.FileName)}";
                    string filePath = Path.Combine(imageDirectory, newFileName);
                    imagePaths.Add(Path.Combine("uploads", rentalId.ToString(), "img", newFileName));

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        using (var image = Image.Load(file.OpenReadStream()))
                        {
                            var encoder = new JpegEncoder { Quality = 75 }; // Set chất lượng ảnh, giá trị từ 1-100
                            image.Save(stream, encoder);
                        }
                    }
                }
            }

            // Lưu đường dẫn ảnh vào cơ sở dữ liệu
            foreach (var imagePath in imagePaths)
            {
                rental.RentalListImages.Add(new RentalListImage { RentalId = rentalId, Sourse = imagePath });
            }

            _context.SaveChanges();

            return RedirectToPage("/Index");
        }
        public static string FormatDescriptionForSave(string description)
        {
            if (string.IsNullOrEmpty(description))
                return description;

            return description.Replace("\r\n", "<br>").Replace("\n", "<br>").Replace("\r", "<br>");
        }
    }


}