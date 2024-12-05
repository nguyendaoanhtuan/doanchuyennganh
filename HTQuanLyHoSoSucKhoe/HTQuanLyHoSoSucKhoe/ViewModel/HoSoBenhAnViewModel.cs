namespace HTQuanLyHoSoSucKhoe.ViewModels
{
    public class HoSoBenhAnViewModel
    {
        public int UserId { get; set; } // ID người dùng
        public string UserName { get; set; } // Tên người dùng (từ bảng User)
        public string Cccd { get; set; } // CCCD (từ bảng User)
        public string PhoneNumber { get; set; } // Số điện thoại (từ bảng User)
        public string Email { get; set; } // Email (từ bảng User)

        public int BenhVienId { get; set; } // ID bệnh viện
        public string TrieuChung { get; set; } // Triệu chứng bệnh
        public string ChanDoan { get; set; } // Chẩn đoán bệnh
        public string ThuocDuocKe { get; set; } // Thuốc được kê
        public string GhiChu { get; set; } // Ghi chú
        public string HinhAnh { get; set; } // Đường dẫn ảnh (nếu có)

        public DateTime NgayTao { get; set; } 
    }
}
