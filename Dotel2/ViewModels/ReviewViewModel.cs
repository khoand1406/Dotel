namespace Dotel2.ViewModels
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RentalId { get; set; }
        public string FullName { get; set; }
    }
}
