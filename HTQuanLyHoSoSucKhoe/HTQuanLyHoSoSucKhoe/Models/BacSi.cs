using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HTQuanLyHoSoSucKhoe.Models;


namespace HTQuanLyHoSoSucKhoe.Models
{
    public class BacSi
    {
        [Key]
        public int Id { get; set; }  // Khóa chính
        public string hoTen { get; set; }
        public DateTime ngaySinh { get; set; }
        public string gioiTinh { get; set; }
        public string soDienThoai { get; set; }
        public string email { get; set; }
        public string diaChi { get; set; }

        [StringLength(255)]
        public string Image_Path { get; set; }

        public int ChuyenKhoaId { get; set; }
        public int BenhVienId { get; set; }

        public DateTime ngayTaoBacSi { get; set; } = DateTime.Now;
        public virtual ChuyenKhoa ChuyenKhoa { get; set; }
        public virtual BenhVien BenhVien { get; set; }


    }
}
