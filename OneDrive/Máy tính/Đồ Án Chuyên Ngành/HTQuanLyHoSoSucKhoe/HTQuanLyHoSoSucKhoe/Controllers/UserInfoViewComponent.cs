using Microsoft.AspNetCore.Mvc;

namespace HTQuanLyHoSoSucKhoe.Controllers
{
    public class UserInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var userName = User.Identity.IsAuthenticated ? User.Identity.Name : null;

            // Trả về view với kiểu dữ liệu rõ ràng là string
            return View<string>("Default", userName);
        }
    }
}
