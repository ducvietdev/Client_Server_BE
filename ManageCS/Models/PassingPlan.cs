using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class PassingPlan
    {
        public int Id { get; set; }
        public int? PlanId { get; set; }
        public string? Note { get; set; }

        public virtual TrainingPlan? Plan { get; set; }
    }
}
