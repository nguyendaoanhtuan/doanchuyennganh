using System.ComponentModel.DataAnnotations;

namespace HTQuanLyHoSoSucKhoe.Models
{
    public class LoaiDichVuKham
    {
        [Key]
        public int Id { get; set; }

        // Tên dịch vụ (ví dụ: Khám tổng quát, Khám chuyên khoa)
        public string TenDichVu { get; set; }

        // Mô tả về dịch vụ thăm khám
        public string MoTa { get; set; }

        // Quan hệ với Chuyên khoa, dịch vụ này có thể liên kết với nhiều chuyên khoa
        public ICollection<LoaiDichVuChuyenKhoa> LoaiDichVuChuyenKhoas { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
