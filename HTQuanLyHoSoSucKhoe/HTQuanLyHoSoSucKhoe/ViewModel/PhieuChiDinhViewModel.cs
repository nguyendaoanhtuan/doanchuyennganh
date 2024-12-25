using Microsoft.AspNetCore.Mvc.Rendering;

namespace HTQuanLyHoSoSucKhoe.ViewModels
{
    public class PhieuChiDinhViewModels
    {
        public int Id { get; set; } // ID của phiếu chỉ định
        public string LoaiChiDinh { get; set; } // Loại chỉ định
        public string? DonThuoc { get; set; } // Thuốc được kê
        public string GhiChu { get; set; } // Ghi chú
        public DateTime Created_At { get; set; } // Ngày tạo phiếu
        public string ChuyenKhoaName { get; set; } // Tên chuyên khoa
        public string PhoneNumber { get; set; } // Số điện thoại bệnh nhân
        public string Cccd {  get; set; }
        public int? UserId { get; set; } // ID của tài khoản (nếu có)
        public int appointmentId { get; set; } // Danh sách AppointmentId

        public int hoSoBenhAnId { get; set; } = 1;
        public int chuyenKhoaId { get; set; }
        public int phieuKetQuaId    { get; set; }

        
        public int bacSiId { get; set; } 

        public string tenBacSi { get; set; }
        public string tenBenhNhan { get; set; } // Họ và tên người dùng
        public DateTime NgayDangKy { get; set; }   // Ngày đăng ký của Appointment

        public int SelectedChuyenKhoaId { get; set; }
        public List<SelectListItem> ChuyenKhoaOptions { get; set; } = new List<SelectListItem>();

        public int SelectedBacSiId { get; set; }
    }
}
