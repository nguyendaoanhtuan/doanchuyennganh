namespace HTQuanLyHoSoSucKhoe.Models
{
    public class PhieuKetQua
    {
        public int Id { get; set; }
        public string? duongDanFile1 { get; set; }
        public string? duongDanFile2 { get; set; }
        public string? duongDanFile3 { get; set; }


        public int phieuChiDinhId { get; set; }
        public virtual PhieuChiDinh PhieuChiDinh { get; set; }
        public int hoSoBenhAnId { get; set; }
        public virtual HoSoBenhAn HoSoBenhAn { get; set; }
        public int chuyenKhoaId { get; set; }
        public virtual ChuyenKhoa ChuyenKhoa { get; set; }
        public int appointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }

        public DateTime Created_At { get; set; } // Ngày tạo phiếu
        public DateTime Updated_At { get; set; } // Ngày cập nhật phiếu

        public int bacSiId  { get; set; }
        public virtual BacSi BacSi { get; set; }

    }
}
