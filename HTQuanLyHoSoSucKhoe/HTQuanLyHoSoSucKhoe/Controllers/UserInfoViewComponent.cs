using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace HTQuanLyHoSoSucKhoe.Controllers
{
    public class UserInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // Lấy full name từ claim 'Name'
            var claimsPrincipal = User as ClaimsPrincipal;
            var fullName = claimsPrincipal?.FindFirst(ClaimTypes.Name)?.Value;

            // Trả về view với kiểu dữ liệu rõ ràng là string
            return View<string>("Default", fullName);
        }
    }
}
