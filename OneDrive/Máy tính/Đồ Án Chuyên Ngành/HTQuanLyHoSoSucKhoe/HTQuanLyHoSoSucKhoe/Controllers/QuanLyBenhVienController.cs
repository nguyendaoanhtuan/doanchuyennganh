using HTQuanLyHoSoSucKhoe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HTQuanLyHoSoSucKhoe.Controllers
{
    [Authorize(AuthenticationSchemes = "AdminScheme")]
    public class QuanLyBenhVienController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuanLyBenhVienController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DangNhap(string taikhoan, string matkhau, string? returnUrl = null)
        {
            // Tìm tài khoản dựa vào tên tài khoản
            var qlbv = _context.TKBenhVien.SingleOrDefault(u => u.Taikhoan == taikhoan);

            // Kiểm tra xem tài khoản và mật khẩu có đúng không
            if (qlbv != null && qlbv.Matkhau == matkhau)
            {
                // Nếu đăng nhập thành công, chuyển hướng đến trang chính của quản lý bệnh viện
                return RedirectToAction("Index", "QuanLyBenhVien");
            }

            // Thông báo lỗi nếu tên đăng nhập hoặc mật khẩu không đúng
            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
            return View();
        }
    }
}
