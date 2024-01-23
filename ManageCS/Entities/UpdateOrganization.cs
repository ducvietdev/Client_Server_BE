namespace ManageCS.Entities
{
    public class UpdateOrganization
    {
        public int Id { get; set; }
        public string? OrganizationCode { get; set; }
        public string? OrganizationName { get; set; }
        public int? Organization_TypeId { get; set; }
        public int? Organization_LevelId { get; set; }
        public int? Organization_ParentId { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
    }
}
