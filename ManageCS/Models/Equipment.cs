using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class Equipment
    {
        public Equipment()
        {
            EquipmentDetails = new HashSet<EquipmentDetail>();
            TrainingPlans = new HashSet<TrainingPlan>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? UnitId { get; set; }
        public int? TypeId { get; set; }
        public string? Quality { get; set; }
        public int? YearUse { get; set; }
        public string? UnitName { get; set; }
        public string? TypeName { get; set; }
        public int? OrganizationId { get; set; }
        public string? OrganizationName { get; set; }

        public virtual Organization? Organization { get; set; }
        public virtual EquipmentType? Type { get; set; }
        public virtual EquipmentUnit? Unit { get; set; }
        public virtual ICollection<EquipmentDetail> EquipmentDetails { get; set; }
        public virtual ICollection<TrainingPlan> TrainingPlans { get; set; }
    }
}
