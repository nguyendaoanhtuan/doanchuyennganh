using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HTQuanLyHoSoSucKhoe.Models
{
    public class LoaiDichVuChuyenKhoa
    {
        [Key]
        public int Id { get; set; }

        // Khóa ngoại liên kết đến Loại dịch vụ thăm khám
        [ForeignKey("LoaiDichVuThamKham")]
        public int LoaiDichVuId { get; set; }

        // Khóa ngoại liên kết đến Chuyên khoa
        [ForeignKey("ChuyenKhoa")]
        public int ChuyenKhoaId { get; set; }

        // Điều hướng đến Loại dịch vụ thăm khám và Chuyên khoa
        public virtual LoaiDichVuKham LoaiDichVuKham { get; set; }
        public virtual ChuyenKhoa ChuyenKhoa { get; set; }
    }
}
