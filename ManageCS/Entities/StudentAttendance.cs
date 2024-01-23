namespace ManageCS.Entities
{
    public class StudentAttendance
    {
        public string Student_Id { get; set; }
        public string? FullName { get; set; }
        public DateTime? Birthday { get; set; }
        public string? ClassName { get; set; }
    }
}
