using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class Attendance
    {
        public int Id { get; set; }
        public int? PlanId { get; set; }
        public string? PlanName { get; set; }
        public string? StudentId { get; set; }
        public string? StudentName { get; set; }
        public int? Buoidihoc { get; set; }
        public double? Comat { get; set; }

        public virtual TrainingPlan? Plan { get; set; }
        public virtual Student? Student { get; set; }
    }
}
