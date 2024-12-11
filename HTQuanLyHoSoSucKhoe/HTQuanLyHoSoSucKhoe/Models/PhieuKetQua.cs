using System.ComponentModel.DataAnnotations;

namespace HTQuanLyHoSoSucKhoe.Models
{
    public class PhieuKetQua
    {
        [Key]
        public int Id { get; set; }
        public string LoaiPhieuId { get; set; }
        public int BacSiId { get; set; }
        public int UserId { get; set; }
        public int BenhVienId { get; set; }
        public string? DonThuoc { get; set; } // Thuốc được kê
        public string? GhiChu { get; set; } // Ghi chú của bác sĩ
        public string DuongDanPhieu { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public DateTime NgayCapNhat { get; set; } = DateTime.Now; // Ngày cập nhật
        public BacSi BacSi { get; set; }
        public LoaiPhieu LoaiPhieu { get; set; }
        public User User { get; set; }
        public BenhVien BenhVien { get; set; }
    }
}
