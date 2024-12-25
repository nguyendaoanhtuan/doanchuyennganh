using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTQuanLyHoSoSucKhoe.Models
{
    public class HoSoBenhAn
    {
        [Key]
        public int Id { get; set; } // Khóa chính

        [ForeignKey("User")]
        public int UserId { get; set; } // Khóa ngoại đến bảng User

        [ForeignKey("BenhVien")]
        public int BenhVienId { get; set; }  // Chỉ giữ lại BenhVienId

        public int chuyenKhoaId { get; set; }
        public virtual ChuyenKhoa ChuyenKhoa { get; set; }

        public int bacSiId { get; set; }
        public virtual BacSi BacSi { get; set; }

        public string? GhiChu { get; set; } // Ghi chú của bác sĩ

        public DateTime ngayTao { get; set; } = DateTime.Now; // Ngày tạo
        public DateTime ngayCapNhat { get; set; } = DateTime.Now; // Ngày cập nhật

        public virtual User User { get; set; }
        public virtual BenhVien BenhVien { get; set; }


        // Thông tin về đơn thuốc
        public virtual ICollection<DonThuoc>? DonThuocs { get; set; }

        // Các thông tin khác về bệnh án (lịch sử thăm khám, kết quả xét nghiệm, ...)

        public virtual ICollection<PhieuChiDinh>? PhieuChiDinhs { get; set; }

        public virtual ICollection<PhieuKetQua>? PhieuKetQuas { get; set; }
    }
}
