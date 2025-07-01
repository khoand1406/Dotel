namespace Dotel2.ViewModels
{
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
}
