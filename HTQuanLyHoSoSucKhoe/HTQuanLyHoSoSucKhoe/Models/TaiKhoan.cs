using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTQuanLyHoSoSucKhoe.Models
{
    public class TaiKhoan
    {
        [Key]
        public int Id { get; set; }

        // Liên kết một-một với User
        public int? UserId { get; set; }
        public virtual User User { get; set; }

        public string passWord { get; set; }

        [NotMapped]
        public string confirmpassword { get; set; }
        // Liên kết một-một với BenhVien
        public int? BenhVienId { get; set; }
        public virtual BenhVien BenhVien { get; set; }

    }
}
