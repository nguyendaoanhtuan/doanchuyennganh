using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HTQuanLyHoSoSucKhoe.Models;


namespace HTQuanLyHoSoSucKhoe.Models
{
    public class ChuyenKhoa
    {
        public int Id { get; set; }

        public int BenhVienId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string phoneNumber { get; set; }

        public virtual BenhVien BenhVien { get; set; }

        public virtual ICollection<BacSi> BacSis { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }

        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
