using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class EquipmentDetail
    {
        public int Id { get; set; }
        public int? EquipmentId { get; set; }
        public int? Amount { get; set; }
        public double? Price { get; set; }
        public string? Description { get; set; }

        public virtual Equipment? Equipment { get; set; }
    }
}
