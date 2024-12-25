namespace HTQuanLyHoSoSucKhoe.ViewModels
{
    public class PhieuKetQuaViewModel
    {
        public int Id { get; set; }
        public int chuyenKhoaId { get; set; }

        public int hoSoBenhAnId { get; set; }


        public int bacSiId { get; set; }
        public int appointmentId { get; set; }

        public int phieuChiDinhId { get; set; }

        public string File1 { get; set; }              // Tên file 1
        public string File2 { get; set; }              // Tên file 2
        public string File3 { get; set; }

        public string TenChuyenKhoaCanLamSang {  get; set; }

        public string TenChuyenKhoaLamSang { get; set;}
        public DateTime Created_At { get; set; }
    }
}
