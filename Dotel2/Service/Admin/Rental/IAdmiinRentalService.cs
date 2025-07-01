using Dotel2.ViewModels;

namespace Dotel2.Service.Admin.Rental
{
    public interface IAdmiinRentalService
    {
        public EditRentalModel? getRentalEdit(int id);

        public void deleteRental(int id);

        public bool UpdateRental(EditRentalModel model);
    }
}
