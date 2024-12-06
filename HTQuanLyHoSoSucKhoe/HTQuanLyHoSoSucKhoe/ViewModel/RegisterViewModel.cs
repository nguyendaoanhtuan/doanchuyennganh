using System.ComponentModel.DataAnnotations;

namespace HTQuanLyHoSoSucKhoe.ViewModels
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string Phone_Number { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string Cccd { get; set; }
        public string Address { get; set; }
        public string Image_Path { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        [Compare("password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.")]
        public string confirmpassword { get; set; }

        public int RoleId { get; set; }
    }
}
