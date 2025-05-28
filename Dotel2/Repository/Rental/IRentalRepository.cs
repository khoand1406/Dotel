


using Dotel2.Models;

namespace Dotel2.Repository.Rental
{
    public interface IRentalRepository
    {
        public Dotel2.Models.Rental GetRental(int id);

        public List<Dotel2.Models.Rental> GetRentals();

        //public List<EXE_Dotel.Models.Rental> GetRentalByFilters();

        public List<Dotel2.Models.Rental> getRentalWithImage(int pagesize);


        public List<Models.Rental> getRentersPaging(List<Dotel2.Models.Rental> rentals, int page, int pageSize);

        public int getListRentalsCount(List<Models.Rental> rentals);

        public Models.Rental getRentalWithListImages(int rentalId);


        public Models.Rental getRentalWithListImagesAndVideo(int rentalId);

        public void getViewCountIncrease(Models.Rental rental);


        public List<Dotel2.Models.Rental> getApprovaledRentals();

        public List<Models.Rental> getFilteredRental(string location, string type, string square, string price);

        public List<Dotel2.Models.Rental> getFilterRentalPaging(string? location, string? type, decimal? maxSquare,
            decimal? minSquare, decimal? minPrice, decimal? maxPrice);

        public List<String> getSuggestLocation(string query);

    }
}
