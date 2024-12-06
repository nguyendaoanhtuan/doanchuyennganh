using System.ComponentModel.DataAnnotations;

namespace HTQuanLyHoSoSucKhoe.ViewModels
{
    public class ThongTinNguoiDungViewModel
    {
        public string Ho { get; set; }
        public string Ten { get; set; } // Tên bệnh viện
        public string soDienThoai { get; set; } // Số điện thoại
        public string cCCD { get; set; }
        public string diaChi { get; set; } // Địa chỉ
        public string duongDanHinhAnh { get; set; } // Đường dẫn hình ảnh


        [Required(ErrorMessage = "Mật khẩu cũ là bắt buộc.")]
        public string matKhauCu { get; set; }

        [Required(ErrorMessage = "Mật khẩu mới là bắt buộc.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Mật khẩu mới phải có ít nhất 6 ký tự.")]
        public string matKhauMoi { get; set; }

        [Required(ErrorMessage = "Nhập lại mật khẩu là bắt buộc.")]
        [Compare("matKhauMoi", ErrorMessage = "Mật khẩu mới và mật khẩu nhập lại không khớp.")]
        public string nhapLaiMatKhau { get; set; }
    }
}
