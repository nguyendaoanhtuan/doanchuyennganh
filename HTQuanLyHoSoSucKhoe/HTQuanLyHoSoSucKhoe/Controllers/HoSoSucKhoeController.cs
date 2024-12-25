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

        
    }
}
