using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTQuanLyHoSoSucKhoe.Models
{
    public class User 
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone_Number { get; set; }
        [Required]
        public string Ho {  get; set; }
        [Required]
        public string Ten {  get; set; }
        [Required]
        public string Cccd { get; set; }

        [NotMapped]
        public string Address { get; set; }
        
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public string Status { get; set; } = "active";
        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime Updated_At { get; set; } = DateTime.Now;

        public virtual ICollection<Appointment> Appointments { get; set; }  // Một User có thể có nhiều Appointment

        public virtual HoSoBenhAn HoSoBenhAn { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }

    }
}
