namespace ManageCS.Entities
{
    public class UpdateEquipmentType
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string IsActive { get; set; }
        public string? Description { get; set; }
    }
}
