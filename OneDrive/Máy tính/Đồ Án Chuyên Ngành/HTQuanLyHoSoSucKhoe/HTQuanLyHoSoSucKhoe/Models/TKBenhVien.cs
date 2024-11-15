using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace HTQuanLyHoSoSucKhoe.Models
{
    public class TKBenhVien
    {
        [Key]
        public int Id { get; set; }

        // Không yêu cầu BenhVienId cho đăng nhập
        [ForeignKey("BenhVien")]
        [ValidateNever]
        public int? BenhVienId { get; set; }  // Dùng kiểu nullable int (int?)

        [Required]
        public string Taikhoan { get; set; }

        [Required]
        public string Matkhau { get; set; }

        public virtual BenhVien BenhVien { get; set; }
    }
}
