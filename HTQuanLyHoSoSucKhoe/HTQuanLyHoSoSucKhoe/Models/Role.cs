namespace HTQuanLyHoSoSucKhoe.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Vaitro { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<BenhVien> BenhViens { get; set; }
    }
}
