using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTQuanLyHoSoSucKhoe.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }  // Chỉ giữ lại UserId

        [ForeignKey("BenhVien")]
        public int BenhVienId { get; set; }  // Chỉ giữ lại BenhVienId


        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone_Number { get; set; }
        public string TenBenhVien { get; set; }
        public DateTime Appointment_Date { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }

        public virtual User User { get; set; }
        public virtual BenhVien BenhVien { get; set; }
    }
}
