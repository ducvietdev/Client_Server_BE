using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class TrainingPlan
    {
        public TrainingPlan()
        {
            Subjects = new HashSet<Subject>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public int? OrganizationId { get; set; }
        public int? EquipmentId { get; set; }
        public int? SoTiet { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? TimeEnd { get; set; }
        public string? Location { get; set; }
        public int? YearId { get; set; }
        public string? YearName { get; set; }
        public int? SemesterId { get; set; }
        public string? Description { get; set; }
        public int? TypeId { get; set; }
        public string? TypeName { get; set; }
        public int? SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? OrganzationName { get; set; }
        public string? SemesterName { get; set; }
        public string? EquipmentName { get; set; }
        public int? Sobuoi { get; set; }

        public virtual Equipment? Equipment { get; set; }
        public virtual Organization? Organization { get; set; }
        public virtual Semester? Semester { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual PlanType? Type { get; set; }
        public virtual Year? Year { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
