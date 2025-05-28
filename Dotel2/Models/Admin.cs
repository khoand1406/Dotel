using System;
using System.Collections.Generic;

namespace Dotel2.Models
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool Status { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
    }
}
