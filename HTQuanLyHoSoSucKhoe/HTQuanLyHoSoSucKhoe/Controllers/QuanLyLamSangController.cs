using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HTQuanLyHoSoSucKhoe.Controllers
{
    [Authorize(Roles = "LamSang")]
    public class QuanLyLamSangController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
