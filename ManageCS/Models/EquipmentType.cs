using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class EquipmentType
    {
        public EquipmentType()
        {
            Equipment = new HashSet<Equipment>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? IsActive { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Equipment> Equipment { get; set; }
    }
}
