using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class EquipmentUnit
    {
        public EquipmentUnit()
        {
            Equipment = new HashSet<Equipment>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Equipment> Equipment { get; set; }
    }
}
