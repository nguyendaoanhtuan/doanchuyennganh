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
        public string trieuChung { get; set; } // Triệu chứng

        public string chanDoan { get; set; } // Chẩn đoán

        public string thuocDuocKe { get; set; } // Thuốc được kê
        public string GhiChu { get; set; } // Ghi chú của bác sĩ

        public string hinhAnh { get; set; }
        public DateTime ngayTao { get; set; } = DateTime.Now; // Ngày tạo
        public DateTime ngayCapNhat { get; set; } = DateTime.Now; // Ngày cập nhật

        public virtual User User { get; set; }
        public virtual BenhVien BenhVien { get; set; }
    }
}
