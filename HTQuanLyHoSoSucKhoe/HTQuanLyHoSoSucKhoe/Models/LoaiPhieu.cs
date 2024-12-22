using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTQuanLyHoSoSucKhoe.Models
{
    public class LoaiPhieu
    {
        [Key]
        public string Id { get; set; }
        public string TenLoai { get; set; }


        // Quan hệ với PhieuChiDinh
        public virtual ICollection<PhieuChiDinh> PhieuChiDinhs { get; set; }

    }
}
