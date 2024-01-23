using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class TableScore
    {
        public int Id { get; set; }
        public string? StudentId { get; set; }
        public int? SubjectId { get; set; }
        public double? Diemchuyencan { get; set; }
        public double? Diemthuongxuyen { get; set; }
        public double? Diemthi { get; set; }
        public double? Diemtbmon { get; set; }
        public int? Sotinchi { get; set; }
        public string? Description { get; set; }
        public string? StudentName { get; set; }
        public string? SubjectName { get; set; }
        public DateTime? Birthday { get; set; }

        public virtual Student? Student { get; set; }
        public virtual Subject? Subject { get; set; }
    }
}
