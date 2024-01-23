namespace ManageCS.Entities
{
    public class InfoResponse
    {
        public string Id { get; set; }
        public string CreditCard { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public int? OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public int? LevelId { get; set; }
        public string LevelName { get; set; }
    }
}
