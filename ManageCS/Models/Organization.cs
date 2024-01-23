using System;
using System.Collections.Generic;

namespace ManageCS.Models
{
    public partial class Organization
    {
        public Organization()
        {
            Classes = new HashSet<Class>();
            Equipment = new HashSet<Equipment>();
            Monitors = new HashSet<Monitor>();
            Students = new HashSet<Student>();
            TrainingPlans = new HashSet<TrainingPlan>();
            UserLogins = new HashSet<UserLogin>();
        }

        public int Id { get; set; }
        public string? OrganizationCode { get; set; }
        public string? OrganizationName { get; set; }
        public int? OrganizationTypeId { get; set; }
        public int? OrganizationLevelId { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public int? OrganizationParentId { get; set; }

        public virtual OrganizationLevel? OrganizationLevel { get; set; }
        public virtual OrganizationType? OrganizationType { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Equipment> Equipment { get; set; }
        public virtual ICollection<Monitor> Monitors { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<TrainingPlan> TrainingPlans { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
    }
}
