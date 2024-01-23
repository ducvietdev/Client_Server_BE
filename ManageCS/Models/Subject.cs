using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Lectures = new HashSet<Lecture>();
            TableScores = new HashSet<TableScore>();
            TrainingPlans = new HashSet<TrainingPlan>();
            Plans = new HashSet<TrainingPlan>();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int? SoTiet { get; set; }
        public DateTime? TimeStart { get; set; }
        public string? Description { get; set; }
        public int? YearId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassId { get; set; }
        public string? YearName { get; set; }
        public string? SemesterName { get; set; }
        public string? ClassName { get; set; }

        public virtual Class? Class { get; set; }
        public virtual Semester? Semester { get; set; }
        public virtual Year? Year { get; set; }
        public virtual ICollection<Lecture> Lectures { get; set; }
        public virtual ICollection<TableScore> TableScores { get; set; }
        public virtual ICollection<TrainingPlan> TrainingPlans { get; set; }

        public virtual ICollection<TrainingPlan> Plans { get; set; }
    }
}
