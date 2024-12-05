using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTQuanLyHoSoSucKhoe.Models
{
    public class ChuyenKhoa
    {
        public int Id { get; set; }

        [ForeignKey("BenhVien")]
        public int BenhVienId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual BenhVien BenhVien { get; set; }
    }
}
