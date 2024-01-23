using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class PlanSubject
    {
        public int PlanId { get; set; }
        public int SubjectId { get; set; }
        public string? Description { get; set; }

        public virtual TrainingPlan Plan { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
    }
}
