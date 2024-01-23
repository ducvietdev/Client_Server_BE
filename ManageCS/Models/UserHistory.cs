using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class UserHistory
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? ActionName { get; set; }
        public DateTime? ActionTime { get; set; }
        public string? ActionContent { get; set; }
        public bool? State { get; set; }

        public virtual UserLogin? User { get; set; }
    }
}
