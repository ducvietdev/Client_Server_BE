using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class Year
    {
        public Year()
        {
            Semesters = new HashSet<Semester>();
            Subjects = new HashSet<Subject>();
            TrainingPlans = new HashSet<TrainingPlan>();
        }

        public int Id { get; set; }
        public string? YearName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? IsActive { get; set; }

        public virtual ICollection<Semester> Semesters { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
        public virtual ICollection<TrainingPlan> TrainingPlans { get; set; }
    }
}
