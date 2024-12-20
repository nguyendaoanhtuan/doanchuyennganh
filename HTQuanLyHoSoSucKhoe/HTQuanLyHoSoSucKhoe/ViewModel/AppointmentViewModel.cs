using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HTQuanLyHoSoSucKhoe.ViewModels
{
    public class AppointmentViewModel
    {
        public string Id { get; set; }
        public int BenhVienId { get; set; }  // ID bệnh viện

        public int UserId { get; set; }
        public string name { get; set; }

      
        [EmailAddress]
        public string email { get; set; }

        [Phone]
        public string phone_Number { get; set; }

        public string cccd { get; set; }

        public string tenBenhVien { get; set; }
        public DateTime Appointment_Date { get; set; }  // Ngày hẹn khám

    
        public TimeSpan Appointment_Time { get; set; }  // Giờ hẹn khám

        public string trangThai { get; set; } = "Đang thăm khám";  // Trạng thái (Pending, Completed, Cancelled)

        public bool taoHoSo {  get; set; }
        public int? soThuTu { get; set; }  // Số thứ tự chờ khám (có thể là null)

        public int LoaiDichVuId { get; set; }
        public string TenDichVu {  get; set; }

        // Các thuộc tính cho việc chọn loại dịch vụ thăm khám và chuyên khoa
        public int? ChuyenKhoaId { get; set; } // ID chuyên khoa (chỉ dùng khi chọn khám chuyên khoa)

        // Danh sách tùy chọn để hiển thị trong giao diện
        public List<SelectListItem> LoaiDichVuList { get; set; }  // Danh sách các loại dịch vụ thăm khám
        public List<SelectListItem> ChuyenKhoaList { get; set; }  // Danh sách chuyên khoa (dành cho khám chuyên khoa)
        public List<SelectListItem> BenhVienList { get; set; } // Danh sách bệnh viện

    }
}
