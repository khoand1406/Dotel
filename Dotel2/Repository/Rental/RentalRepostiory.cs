
using Dotel2.Models;
using Dotel2.Repository.Rental;
using Microsoft.EntityFrameworkCore;

namespace EXE_Dotel.Repository.Rental
{
    public class RentalRepostiory : IRentalRepository
    {
        DotelDBContext dBContext;
        public RentalRepostiory(DotelDBContext context) { dBContext = context; }

        private int pagesize { get; set; } = 6;
        public List<Dotel2.Models.Rental> getFilteredRental(string location, string type, string square, string price)
        {
            List<Dotel2.Models.Rental> rentals = getRentalWithImage(pagesize);

            // Filter by location
            if (!string.IsNullOrEmpty(location))
            {
                rentals = rentals.Where(rental => rental.Location.Contains(location)).ToList();
            }

            // Filter by type
            if (!string.IsNullOrEmpty(type))
            {
                rentals = rentals.Where(rental => rental.Type.Contains(type)).ToList();
            }

            // Filter by area range
            if (!string.IsNullOrEmpty(square))
            {
                var range = square.Split('-');
                if (range.Length == 2 && decimal.TryParse(range[0], out var minArea) && decimal.TryParse(range[1], out var maxArea))
                {
                    rentals = rentals.Where(rental => rental.RoomArea >= minArea && rental.RoomArea <= maxArea).ToList();
                }
                else if (square.Contains("Dưới"))
                {
                    if (decimal.TryParse(square.Split(' ')[1].Replace("m2", "").Trim(), out var upperbound))
                    {
                        rentals = rentals.Where(rental => rental.RoomArea < upperbound).ToList();
                    }
                }
                else if (square.Contains("Trên"))
                {
                    if (decimal.TryParse(square.Split(' ')[1].Replace("m2", "").Trim(), out var lowerbound))
                    {
                        rentals = rentals.Where(rental => rental.RoomArea > lowerbound).ToList();
                    }
                }
            }

            // Filter by price range
            if (!string.IsNullOrEmpty(price))
            {
                var ranges = price.Split('-');
                if (ranges.Length == 2 && decimal.TryParse(ranges[0].Replace(".", "").Trim(), out var minPrice) && decimal.TryParse(ranges[1].Replace(".", "").Trim(), out var maxPrice))
                {
                    rentals = rentals.Where(r => r.Price >= minPrice && r.Price <= maxPrice).ToList();
                }
                else if (price.Contains("Dưới"))
                {
                    if (decimal.TryParse(price.Split(' ')[1].Replace(".", "").Trim(), out var upper))
                    {
                        rentals = rentals.Where(r => r.Price < upper).ToList();
                    }
                }
                else if (price.Contains("Trên"))
                {
                    if (decimal.TryParse(price.Split(' ')[1].Replace(".", "").Trim(), out var lower))
                    {
                        rentals = rentals.Where(r => r.Price > lower).ToList();
                    }
                }
            }

            return rentals;
        }

        public List<Dotel2.Models.Rental> getFilterRentalPaging(string? location, string? type, decimal? maxSquare, decimal? minSquare, decimal? minPrice,decimal? maxPrice)
        {
            List<Dotel2.Models.Rental> rentals = dBContext.Rentals.Include(e => e.RentalListImages).Where(e =>
                    (string.IsNullOrEmpty(location) || e.Location.Contains(location)) &&
                    (string.IsNullOrEmpty(type) || e.Type.Equals(type)) &&
                    (!minSquare.HasValue || e.RoomArea >= minSquare) && 
                    (maxSquare == null || e.RoomArea <= maxSquare) &&
                    (minPrice == null || e.Price >= minPrice) && 
                    (maxPrice == null || e.Price <= maxPrice)
            ).ToList();

            return rentals;

        }

        public int getListRentalsCount(List<Dotel2.Models.Rental> rentals)
        {
            return rentals.Count();
        }

        public Dotel2.Models.Rental GetRental(int rentalId)
        {
            Console.WriteLine($"Fetching rental with ID: {rentalId}");

            var rental = dBContext.Rentals
                                 .Include(r => r.RentalListImages)
                                 .Include(r=> r.RentalVideos).AsSplitQuery()
                                 .FirstOrDefault(r => r.RentalId == rentalId);

            if (rental == null)
            {
                // Debugging: Log if rental not found
                Console.WriteLine($"No rental found for ID: {rentalId}");
            }

            return rental;
        }

        public List<Dotel2.Models.Rental> GetRentals()
        {
            return dBContext.Rentals.ToList();
        }

        public List<Dotel2.Models.Rental> getRentalWithImage(int pagesize)
        {

            return dBContext.Rentals
            .Include(r => r.RentalListImages)
            .OrderByDescending(r=>r.ViewNumber)
            .Where(rental=>rental.Approval==true)
            .Take(pagesize)
            .ToList();
            ;
        }

        public Dotel2.Models.Rental getRentalWithListImages(int rentalId)
        {
            return dBContext.Rentals.Include(r => r.RentalListImages).FirstOrDefault(rental => rental.RentalId == rentalId);
        }

        public Dotel2.Models.Rental getRentalWithListImagesAndVideo(int rentalId)
        {
            return dBContext.Rentals.Include(r => r.RentalListImages)
                                    .Include(rental => rental.RentalVideos).AsSplitQuery()
                                    .FirstOrDefault(rental=> rental.RentalId==rentalId);
        }

        public List<Dotel2.Models.Rental> getRentersPaging(List<Dotel2.Models.Rental> rentals, int page, int pageSize)
        {
            return rentals
                .Skip((page - 1) * pageSize)
                .Take(pageSize).OrderBy(re=>re.Price).Where(rental=> rental.Approval==true)
                .ToList();
        }

        public void getViewCountIncrease(Dotel2.Models.Rental rental)
        {
            if(rental == null) return;
            else
            {
                rental.ViewNumber += 1;
                dBContext.SaveChanges();
            }
        }
        public List<Dotel2.Models.Rental> getApprovaledRentals()
        {
            var rentals = dBContext.Rentals.Where(r => r.Approval).ToList();
            return rentals;
        }
    }
}
