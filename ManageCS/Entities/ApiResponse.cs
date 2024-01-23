namespace ManageCS.Entities
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public object DataInfo { get; set; }
    }
}
