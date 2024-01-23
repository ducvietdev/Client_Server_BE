namespace ManageCS.Entities
{
    public class UpdateEquipment
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Equipment_UnitId { get; set; }
        public int Equipment_TypeId { get; set; }
        public int Organization_Id { get; set; }
        public string? Quality { get; set; }
        public int YearUse { get; set; }
    }
}
