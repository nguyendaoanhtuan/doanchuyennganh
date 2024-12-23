using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HTQuanLyHoSoSucKhoe.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuanLyDonThuocController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
