using System.ComponentModel.DataAnnotations.Schema;

namespace HTQuanLyHoSoSucKhoe.Models
{
    public class PhieuChiDinh
    {
        public int Id { get; set; } // Khóa chính

        [ForeignKey("HoSoBenhAn")]
        public int HoSoBenhAnId { get; set; } // Khóa ngoại đến HoSoBenhAn

        public int chuyenKhoaId { get; set; }

        public virtual ChuyenKhoa ChuyenKhoa { get; set; }
        public virtual HoSoBenhAn HoSoBenhAn { get; set; } // Điều hướng đến HoSoBenhAn

        public string LoaiChiDinh { get; set; } // Loại chỉ định (xét nghiệm, chẩn đoán hình ảnh, ...)

        public string? DonThuoc { get; set; } // Thuốc được kê
        public string GhiChu { get; set; } // Ghi chú thêm (nếu có)
        public DateTime Created_At { get; set; } // Ngày tạo phiếu
        public DateTime Updated_At { get; set; } // Ngày cập nhật phiếu

        // Quan hệ với các kết quả (nếu cần)
        public virtual ICollection<PhieuKetQua> PhieuKetQuas { get; set; }
    }
}
