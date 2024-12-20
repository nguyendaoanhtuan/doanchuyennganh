using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTQuanLyHoSoSucKhoe.Models
{
    public class Appointment
    {
        public int Id { get; set; } // Khóa chính

        [ForeignKey("User")]
        public int UserId { get; set; } // Khóa ngoại từ User
        public virtual User User { get; set; } // Điều hướng đến User

        [ForeignKey("BenhVien")]
        public int BenhVienId { get; set; } // Khóa ngoại từ BenhVien
        public virtual BenhVien BenhVien { get; set; } // Điều hướng đến BenhVien

        [ForeignKey("HoSoBenhAn")]
        public int? HoSoBenhAnId { get; set; } // Khóa ngoại từ HoSoBenhAn (nullable nếu hồ sơ chưa được tạo)
        public virtual HoSoBenhAn HoSoBenhAn { get; set; } // Điều hướng đến HoSoBenhAn

        public bool taoHoSo { get; set; }

        // Thêm thông tin dịch vụ thăm khám
        [ForeignKey("LoaiDichVuThamKham")]
        public int LoaiDichVuId { get; set; }
        public virtual LoaiDichVuKham LoaiDichVuKham { get; set; }

        // Nếu dịch vụ là khám chuyên khoa, chọn chuyên khoa
        [ForeignKey("ChuyenKhoa")]
        public int? ChuyenKhoaId { get; set; }  // Có thể là null nếu không phải khám chuyên khoa
        public virtual ChuyenKhoa ChuyenKhoa { get; set; }
        public string? trangThaiPhieu { get; set; }
        public DateTime Appointment_Date { get; set; } // Ngày đặt khám
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public DateTime NgayCapNhat { get; set; } = DateTime.Now; // Ngày cập nhật

        // Thêm số thứ tự vào cuộc hẹn trong một ngày
        public int soThuTu { get; set; }
    }
}
