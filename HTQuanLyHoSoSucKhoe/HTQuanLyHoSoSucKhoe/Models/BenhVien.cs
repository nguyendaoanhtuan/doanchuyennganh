using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTQuanLyHoSoSucKhoe.Models
{
    public class BenhVien
    {
        [Key]
        public int Id { get; set; }  // Khóa chính

        [Required]
        [StringLength(100)]
        public string Name { get; set; }  // Tên bệnh viện

        [Required]
        [StringLength(255)]
        public string Address { get; set; }  // Địa chỉ bệnh viện

        [StringLength(15)]
        public string Phone_Number { get; set; }  // Số điện thoại liên hệ

        [StringLength(255)]
        public string Image_Path { get; set; }  // Đường dẫn ảnh


        public string Description { get; set; }  // Mô tả chi tiết về bệnh viện


        public int RoleId { get; set; } // Khóa ngoại đến bảng Role
        public virtual Role Role { get; set; }

        public DateTime Created_At { get; set; } = DateTime.Now;  // Ngày tạo

        public DateTime Updated_At { get; set; } = DateTime.Now;  // Ngày cập nhật

        public virtual ICollection<Appointment> Appointments { get; set; }  // Một BenhVien có thể có nhiều Appointment

        public virtual ICollection<ChuyenKhoa> ChuyenKhoas { get; set; }  // Một BenhVien có thể có nhiều ChuyenKhoa

        public virtual ICollection<HoSoBenhAn> HoSoBenhAns { get; set; } // Một BenhVien có thể có nhiều HoSoBenhAn

        public virtual TaiKhoan TaiKhoan { get; set; }

        public virtual ICollection<BacSi> BacSis { get; set; }
    }
}
