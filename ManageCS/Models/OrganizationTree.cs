namespace ManageCS.Models
{
    public class OrganizationTree
    {
        public int id { get; set; }
        public string? organization_code { get; set; }
        public string? organization_name { get; set; }
        public int? organization_typeId { get; set; }
        public int? organization_levelId { get; set; }
        public string? description { get; set; }
        public string? address { get; set; }
        public int? organization_parentId { get; set; }
        public List<OrganizationTree> children { get; set; }
    }
}
