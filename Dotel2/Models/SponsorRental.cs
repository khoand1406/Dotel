using System;
using System.Collections.Generic;

namespace Dotel2.Models
{
    public partial class SponsorRental
    {
        public int SponsorRentalId { get; set; }
        public int RentalId { get; set; }
        public int SponsorId { get; set; }

        public virtual Rental Rental { get; set; } = null!;
        public virtual Sponsorship Sponsor { get; set; } = null!;
    }
}
