using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class UserLogin
    {
        public UserLogin()
        {
            UserHistories = new HashSet<UserHistory>();
        }

        public string Id { get; set; } = null!;
        public string? CreditCard { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int? StateId { get; set; }
        public string? StateName { get; set; }
        public int? OrganizationId { get; set; }
        public string? OrganizationName { get; set; }
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
        public int? LevelId { get; set; }
        public string? LevelName { get; set; }
        public string? ResetToken { get; set; }

        public virtual OrganizationLevel? Level { get; set; }
        public virtual Organization? Organization { get; set; }
        public virtual Role? Role { get; set; }
        public virtual UserState? State { get; set; }
        public virtual ICollection<UserHistory> UserHistories { get; set; }
    }
}
