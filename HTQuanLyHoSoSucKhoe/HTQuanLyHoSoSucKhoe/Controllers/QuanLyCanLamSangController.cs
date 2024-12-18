using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HTQuanLyHoSoSucKhoe.Controllers
{
    [Authorize(Roles = "CanLamSang")]
    public class QuanLyCanLamSangController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
