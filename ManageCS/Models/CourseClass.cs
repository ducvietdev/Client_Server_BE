using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class CourseClass
    {
        public int CourseId { get; set; }
        public int ClassId { get; set; }
        public string? Description { get; set; }

        public virtual Class Class { get; set; } = null!;
        public virtual Course Course { get; set; } = null!;
    }
}
