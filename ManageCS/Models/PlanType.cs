using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class PlanType
    {
        public PlanType()
        {
            TrainingPlans = new HashSet<TrainingPlan>();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<TrainingPlan> TrainingPlans { get; set; }
    }
}
