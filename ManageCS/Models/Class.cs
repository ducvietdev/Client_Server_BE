using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class Class
    {
        public Class()
        {
            Monitors = new HashSet<Monitor>();
            Students = new HashSet<Student>();
            Subjects = new HashSet<Subject>();
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public string? ClassName { get; set; }
        public string? ClassCode { get; set; }
        public int? QuantityStudent { get; set; }
        public int? OrganizationId { get; set; }
        public string? Description { get; set; }
        public string? YearName { get; set; }
        public string? OrganzationName { get; set; }

        public virtual Organization? Organization { get; set; }
        public virtual ICollection<Monitor> Monitors { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
