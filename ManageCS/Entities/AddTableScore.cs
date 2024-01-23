namespace ManageCS.Entities
{
    public class AddTableScore
    {
        public string Student_Id { get; set; }
        public int Subject_Id { get; set; }
        public double DiemChuyenCan { get; set; }
        public double DiemThuongXuyen { get; set; }
        public double DiemThi { get; set; }
        public int? SoTinChi { get; set; }
        public string Description { get; set; }
    }
}
