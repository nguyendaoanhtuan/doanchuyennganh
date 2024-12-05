using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HTQuanLyHoSoSucKhoe.Controllers
{
    public class HoSoSucKhoeController : Controller
    {

     
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LichSuKhamBenh()
        {
            return View();
        }
        public IActionResult LichSuKeThuoc()
        {
            return View();
        }
        public IActionResult LichSuTiemPhong()
        {
            return View();
        }
        public IActionResult LichSuDatLichKham()
        {
            return View();
        }
        public IActionResult LichSuDatLichTiemPhong()
        {
            return View();
        }
    }
}
