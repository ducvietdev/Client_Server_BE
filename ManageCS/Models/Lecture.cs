using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class Lecture
    {
        public int Id { get; set; }
        public string? LectureName { get; set; }
        public int? Sotiet { get; set; }
        public string? Description { get; set; }
        public int? SubjectId { get; set; }
        public string? SubjectName { get; set; }

        public virtual Subject? Subject { get; set; }
    }
}
