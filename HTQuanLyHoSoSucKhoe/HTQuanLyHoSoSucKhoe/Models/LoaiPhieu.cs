using System.ComponentModel.DataAnnotations;

namespace HTQuanLyHoSoSucKhoe.Models
{
    public class LoaiPhieu
    {
        [Key]
        public string Id { get; set; }
        public string TenLoai { get; set; }
        public ICollection<PhieuKetQua> PhieuKetQuas { get; set; }

    }
}
