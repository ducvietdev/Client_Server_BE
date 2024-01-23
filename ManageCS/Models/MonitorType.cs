using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class MonitorType
    {
        public MonitorType()
        {
            Monitors = new HashSet<Monitor>();
        }

        public int Id { get; set; }
        public string? MonitorTypeCode { get; set; }
        public string? MonitorTypeName { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Monitor> Monitors { get; set; }
    }
}
