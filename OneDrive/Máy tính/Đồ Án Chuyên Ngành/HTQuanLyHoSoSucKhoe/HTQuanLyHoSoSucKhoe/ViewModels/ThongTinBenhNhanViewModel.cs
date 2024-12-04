using HTQuanLyHoSoSucKhoe.Models;
using System.ComponentModel.DataAnnotations;

namespace HTQuanLyHoSoSucKhoe.ViewModels
{
	public class ThongTinBenhNhanViewModel
	{
		public string cccd { get; set; }
		public string hoTen { get; set; }
		[DataType(DataType.Date)]
		public DateTime ngaySinh { get; set; }
		public string gioiTinh { get; set; }
		public string sdt { get; set; }
		public string email { get; set; }
	}
}
