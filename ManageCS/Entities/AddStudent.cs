namespace ManageCS.Entities
{
    public class AddStudent
    {
        public string Student_Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }
        public string Rank { get; set; }
        public int Organization_Id { get; set; }
        public int Course_Id { get; set; }
        public int Class_Id { get; set; }
    }
}
