using System;
using System.Collections.Generic;

namespace Dotel2.Models
{
    public partial class Role
    {
        public Role()
        {
            Admins = new HashSet<Admin>();
            Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;

        public virtual ICollection<Admin> Admins { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
