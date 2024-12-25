using Microsoft.AspNetCore.Mvc.Rendering;

namespace HTQuanLyHoSoSucKhoe.ViewModel
{
    public class DanhSachHenKhamViewModel
    {
        public int id { get; set; }
        public int soThuTu { get; set; }
        public string cccd { get; set; }
        public string tenBenhNhan { get; set; }
        public DateTime thoiGianTao { get; set; }
        public string trangThai { get; set; }

        public int hoSoBenhAnId { get; set; }
        public int SelectedChuyenKhoaId { get; set; }
        public List<SelectListItem> ChuyenKhoaOptions { get; set; } = new List<SelectListItem>();

        public int SelectedBacSiId { get; set; }
        public string loaiPhieuChiDinh { get; set; }
    }
}
