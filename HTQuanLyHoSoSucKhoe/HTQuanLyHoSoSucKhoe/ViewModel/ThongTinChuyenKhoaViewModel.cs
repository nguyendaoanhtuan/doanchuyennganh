using System.ComponentModel.DataAnnotations;

namespace HTQuanLyHoSoSucKhoe.ViewModels
{
    public class ThongTinChuyenKhoaViewModel
    {
        public int chuyenKhoaId { get; set; }
        public string tenChuyenKhoa { get; set; }

        public int BenhVienId { get; set; }

        public string tenBenhVien {  get; set; }

        public string soTaiKhoan { get; set; }

        public string matKhau {  get; set; }

        [Required(ErrorMessage = "Mật khẩu cũ là bắt buộc.")]
        public string matKhauCu { get; set; }

        [Required(ErrorMessage = "Mật khẩu mới là bắt buộc.")]
    
        public string matKhauMoi { get; set; }

        [Required(ErrorMessage = "Nhập lại mật khẩu là bắt buộc.")]
        [Compare("matKhauMoi", ErrorMessage = "Mật khẩu mới và mật khẩu nhập lại không khớp.")]
        public string nhapLaiMatKhau { get; set; }
    }
}
