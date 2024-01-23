namespace ManageCS.Entities
{
    public class UpdateTrainingPlan
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? SoTiet { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? TimeEnd { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public int? TongSoBuoi { get; set; }
        public int? Year_Id { get; set; }
        public int? Type_Id { get; set; }
        public int? Semester_Id { get; set; }
        public int? Organization_Id { get; set; }
        public int? Equipment_Id { get; set; }
        public int? Subject_Id { get; set; }
    }
}
