using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HTQuanLyHoSoSucKhoe.Models
{
    public class HoSoKhamBenh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public DateTime ngayKhoiTao { get; set; }
        public int userId { get; set; }
        public virtual User User { get; set; }
    }
}
