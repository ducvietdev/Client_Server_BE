using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class UserState
    {
        public UserState()
        {
            UserLogins = new HashSet<UserLogin>();
        }

        public int Id { get; set; }
        public string? StateName { get; set; }
        public string? StateCode { get; set; }

        public virtual ICollection<UserLogin> UserLogins { get; set; }
    }
}
