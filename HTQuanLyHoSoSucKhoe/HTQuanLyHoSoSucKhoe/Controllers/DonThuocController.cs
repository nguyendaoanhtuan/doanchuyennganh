using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HTQuanLyHoSoSucKhoe.Controllers
{
    [Authorize(Roles = "User")]
    public class DonThuocController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
