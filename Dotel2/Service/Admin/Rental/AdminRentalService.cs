using Dotel2.Repository.Rental;
using Dotel2.Utils;
using Dotel2.ViewModels;

namespace Dotel2.Service.Admin.Rental
{
    public class AdminRentalService : IAdmiinRentalService
    {
        private readonly IRentalRepository _rentalRepo;

        public AdminRentalService(IRentalRepository rentalRepo)
        {
            _rentalRepo = rentalRepo;
        }

        public bool deleteRental(int id)
        {
            var rental = _rentalRepo.GetRental(id);
            if (rental == null) return false;

            _rentalRepo.deleteRental(id);
            _rentalRepo.Save();
            return true;
        }

        public EditRentalModel? getRentalEdit(int id)
        {
            var rental = _rentalRepo.GetRental(id);
            if (rental == null) return null;

            return new EditRentalModel
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
        }

        public bool UpdateRental(EditRentalModel model)
        {
            var rental = _rentalRepo.GetRental(model.RentalId);
            if (rental == null) return false;

            if (ValidateUtils.IsValidPhone(model.ContactPhoneNumber))
            {
                return false;
            }

            rental.RentalTitle = model.RentalTitle;
            rental.Description = model.Description;
            rental.Price = (decimal)model.Price;
            rental.RoomArea = model.RoomArea;
            rental.MaxPeople = model.MaxPeople;
            rental.ContactPhoneNumber = model.ContactPhoneNumber;
            rental.UserId = (int)model.UserId;
            rental.ViewNumber = model.ViewNumber;
            rental.Bathroom = model.Bathroom;
            rental.Kitchen = model.Kitchen;
            rental.BedroomNumber = model.BedroomNumber;
            rental.Location = model.Location;
            rental.GoogleMap = model.GoogleMap;
            rental.Approval = model.Approval;
            rental.Status = model.Status;
            rental.Type = model.Type;

            _rentalRepo.UpdateRental(rental);
            _rentalRepo.Save();
            return true;
        }
    }
}
