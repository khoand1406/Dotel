
using Dotel2.Repository.Rental;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Text.RegularExpressions;

namespace Dotel2.Service.Rental
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _repository;
        private readonly IWebHostEnvironment _env;

        public RentalService(IRentalRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public bool IsPhoneValid(string phone)
        {
            return Regex.IsMatch(phone, @"^0\d{9}$");
        }

        public string FormatDescription(string description)
        {
            return description.Replace("\r\n", "<br>").Replace("\n", "<br>").Replace("\r", "<br>");
        }

        public async Task<bool> CreateRentalAsync(Models.Rental rental, List<IFormFile> mediaFiles)
        {
            rental.Description = FormatDescription(rental.Description);
            int rentalId = await _repository.AddRentalAsync(rental);

            string imageFolder = Path.Combine(_env.WebRootPath, "uploads", rentalId.ToString(), "img");
            Directory.CreateDirectory(imageFolder);

            var savedPaths = new List<string>();
            int count = 1;

            foreach (var file in mediaFiles)
            {
                if (file.Length > 0 && file.ContentType.StartsWith("image/"))
                {
                    string fileName = $"{count++}{Path.GetExtension(file.FileName)}";
                    string filePath = Path.Combine(imageFolder, fileName);
                    savedPaths.Add(Path.Combine("uploads", rentalId.ToString(), "img", fileName));

                    using var stream = new FileStream(filePath, FileMode.Create);
                    using var image = Image.Load(file.OpenReadStream());
                    image.Save(stream, new JpegEncoder { Quality = 75 });
                }
            }

            await _repository.AddImagesAsync(rentalId, savedPaths);
            return true;
        }

        public Models.Rental GetRental(int id)
        {
            return _repository.GetRental(id);
        }

        public List<Models.Rental> GetRentals()
        {
            return _repository.GetRentals();
        }

        public List<Models.Rental> getRentalWithImage(int pagesize)
        {
            return _repository.getRentalWithImage(pagesize);
        }

        public List<Models.Rental> getRentersPaging(List<Models.Rental> rentals, int page, int pageSize)
        {
            return _repository.getRentersPaging(rentals, page, pageSize);
        }

        public int getListRentalsCount(List<Models.Rental> rentals)
        {
            return _repository.getListRentalsCount(rentals);
        }

        public Models.Rental getRentalWithListImages(int rentalId)
        {
            return _repository.getRentalWithListImages(rentalId);
        }

        public Models.Rental getRentalWithListImagesAndVideo(int rentalId)
        {
            return _repository.getRentalWithListImagesAndVideo(rentalId);
        }

        public void getViewCountIncrease(Models.Rental rental)=> _repository.getViewCountIncrease(rental);
        

        public List<Models.Rental> getApprovaledRentals()
        {
            return _repository.getApprovaledRentals();
        }

        public List<Models.Rental> getFilteredRental(string location, string type, string square, string price)
        {
            return _repository.getFilteredRental(location, type, square, price);  
        }

        public List<Models.Rental> getFilterRentalPaging(string? location, string? type, decimal? maxSquare, decimal? minSquare, decimal? minPrice, decimal? maxPrice)
        {
            return _repository.getFilterRentalPaging(location, type, maxSquare, minSquare, minPrice, maxPrice);
        }

        public List<string> getSuggestLocation(string query)
        {
            return _repository.getSuggestLocation(query);
        }
    }
}
