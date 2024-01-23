using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class Semester
    {
        public Semester()
        {
            Subjects = new HashSet<Subject>();
            TrainingPlans = new HashSet<TrainingPlan>();
        }

        public int Id { get; set; }
        public string? SemesterName { get; set; }
        public int? YearId { get; set; }

        public virtual Year? Year { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
        public virtual ICollection<TrainingPlan> TrainingPlans { get; set; }
    }
}
