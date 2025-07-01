namespace Dotel2.Service.Rental
{
    public interface IRentalService
    {
        Task<bool> CreateRentalAsync(Models.Rental rental, List<IFormFile> mediaFiles);
        string FormatDescription(string description);
        bool IsPhoneValid(string phone);

        public Models.Rental GetRental(int id);
        public List<Models.Rental> GetRentals();

        public List<Models.Rental> getRentalWithImage(int pagesize);


        public List<Models.Rental> getRentersPaging(List<Models.Rental> rentals, int page, int pageSize);

        public int getListRentalsCount(List<Models.Rental> rentals);

        public Models.Rental getRentalWithListImages(int rentalId);


        public Models.Rental getRentalWithListImagesAndVideo(int rentalId);

        public void getViewCountIncrease(Models.Rental rental);


        public List<Models.Rental> getApprovaledRentals();

        public List<Models.Rental> getFilteredRental(string location, string type, string square, string price);

        public List<Dotel2.Models.Rental> getFilterRentalPaging(string? location, string? type, decimal? maxSquare,
            decimal? minSquare, decimal? minPrice, decimal? maxPrice);

        public List<String> getSuggestLocation(string query);
    }
}
