using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class Monitor
    {
        public string Id { get; set; } = null!;
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public bool? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Rank { get; set; }
        public string? Position { get; set; }
        public int? OrganizationId { get; set; }
        public int? CourseId { get; set; }
        public int? ClassId { get; set; }
        public int? MonitorTypeid { get; set; }
        public string? MonitorTypename { get; set; }
        public string? ClassName { get; set; }
        public string? OrganizationName { get; set; }
        public string? CourseName { get; set; }

        public virtual Class? Class { get; set; }
        public virtual Course? Course { get; set; }
        public virtual MonitorType? MonitorType { get; set; }
        public virtual Organization? Organization { get; set; }
    }
}
