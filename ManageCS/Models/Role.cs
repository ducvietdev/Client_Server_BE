using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class Role
    {
        public Role()
        {
            UserLogins = new HashSet<UserLogin>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? IsActive { get; set; }
        public DateTime? ActivatedDate { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<UserLogin> UserLogins { get; set; }
    }
}
