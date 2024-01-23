using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class Course
    {
        public Course()
        {
            Monitors = new HashSet<Monitor>();
            Students = new HashSet<Student>();
            Classes = new HashSet<Class>();
        }

        public int Id { get; set; }
        public string? CourseName { get; set; }
        public string? Description { get; set; }
        public int? CountYear { get; set; }

        public virtual ICollection<Monitor> Monitors { get; set; }
        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
