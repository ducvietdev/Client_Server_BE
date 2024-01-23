namespace ManageCS.Entities
{
    public class UpdateUserLogin
    {
        public string Id { get; set; }
        public string CreditCard { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; }
        public int State_Id { get; set; }
        public int Organization_Id { get; set; }
        public int Role_Id { get; set; }
        public int Level_Id { get; set; }
    }
}
