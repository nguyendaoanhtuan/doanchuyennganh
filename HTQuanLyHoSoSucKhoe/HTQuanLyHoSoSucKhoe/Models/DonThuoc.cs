using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HTQuanLyHoSoSucKhoe.Models
{
    public class DonThuoc
    {
        [Key]
        public int Id { get; set; }
        public int hoSoBenhAnId { get; set; }

        public int bacSiId { get; set; }

        public virtual BacSi BacSi { get; set; }
        public virtual HoSoBenhAn HoSoBenhAn { get; set; }

        // Khóa ngoại liên kết đến Phiếu Kết Quả
        public int PhieuKetQuaId { get; set; }

        // Đường dẫn tới tài liệu hình ảnh hoặc file PDF của đơn thuốc
        public string? DuongDanDonThuoc { get; set; }

        // Trạng thái của đơn thuốc (ví dụ: đã cấp, chưa cấp)
        public string? TrangThai { get; set; }

        // Điều hướng đến bảng Phiếu Kết Quả
        public virtual PhieuKetQua PhieuKetQua { get; set; }


    }
}
