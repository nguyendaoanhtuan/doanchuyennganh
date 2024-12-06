namespace HTQuanLyHoSoSucKhoe.ViewModels
{
    public class ThongTinBacSiViewModel
    {
        public int bacSiId { get; set; }

        public string hoTen { get; set; }


        public DateTime ngaySinh { get; set; }


        public string gioiTinh { get; set; }


        public string soDienThoai { get; set; }


        public string email { get; set; }


        public string diaChi { get; set; }

        public int ChuyenKhoaId { get; set; }

        public string tenChuyenKhoa { get; set; }

        public int BenhVienId { get; set; }

        public string tenBenhVien { get; set; }

        public DateTime ngayTaoBacSi { get; set; } = DateTime.Now;


        public string Image_Path { get; set; }
    }
}
