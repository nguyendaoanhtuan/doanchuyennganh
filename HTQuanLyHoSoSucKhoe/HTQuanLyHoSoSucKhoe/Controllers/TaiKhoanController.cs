using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using BCrypt.Net;
using HTQuanLyHoSoSucKhoe.Models;
using HTQuanLyHoSoSucKhoe.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using HTQuanLyHoSoSucKhoe.ViewModels;
namespace HTQuanLyHoSoSucKhoe.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaiKhoanController(ApplicationDbContext context) // Sửa lại để dùng constructor
        {
            _context = context; // Gán giá trị context cho biến _contexts
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(string phone_number, string password)
        {
            // Kiểm tra số điện thoại trong bảng User
            var user = await _context.Users.Include(u => u.Role)
                                           .Include(u => u.TaiKhoan)
                                           .SingleOrDefaultAsync(u => u.Phone_Number == phone_number);

            // Nếu không tìm thấy trong bảng User, kiểm tra bảng BenhVien
            var benhVien = await _context.BenhVien.Include(bv => bv.Role)
                                                  .Include(u => u.TaiKhoan)
                                                  .SingleOrDefaultAsync(bv => bv.Phone_Number == phone_number);

            // Nếu không tìm thấy trong bảng User, kiểm tra bảng BenhVien
            var chuyenKhoa = await _context.ChuyenKhoas.Include(ck => ck.Role)
                                                  .Include(u => u.TaiKhoan)
                                                  .SingleOrDefaultAsync(ck => ck.phoneNumber == phone_number);


            if (user != null && user.TaiKhoan != null && user.Role != null &&
                    (BCrypt.Net.BCrypt.Verify(password, user.TaiKhoan.passWord) || password == user.TaiKhoan.passWord))
            {
                return await AuthenticateAndRedirect(user.Id, user.Ho, user.Ten, user.Role.Vaitro);
            }

            // Xử lý đăng nhập cho Bệnh viện
            if (benhVien != null && benhVien.TaiKhoan != null && benhVien.Role != null && password == benhVien.TaiKhoan.passWord)
            {
                return await AuthenticateAndRedirect(benhVien.Id, benhVien.Name, benhVien.Role.Vaitro);
            }
            
            // Xử lý đăng nhập cho Bệnh viện
            if (chuyenKhoa != null && chuyenKhoa.TaiKhoan != null && chuyenKhoa.Role != null && password == chuyenKhoa.TaiKhoan.passWord)
            {
                return await AuthenticateAndRedirect(chuyenKhoa.Id, chuyenKhoa.Name, chuyenKhoa.Role.Vaitro);
            }

            ModelState.AddModelError("", "Số điện thoại hoặc mật khẩu không đúng.");
            return View();
        }

        // dành cho user để lấy thông tin vào claim
        private async Task<IActionResult> AuthenticateAndRedirect(int id, string ho, string ten, string role)
        {
            var fullName = $"{ho} {ten}"; // Kết hợp họ và tên
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, fullName),
                new Claim(ClaimTypes.Role, role)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return RedirectToAction("Index", "Home");
        }

        //Dành cho bệnh viện để lấy thông tin vào claim
        private async Task<IActionResult> AuthenticateAndRedirect(int id, string fullName, string role)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, fullName),
                new Claim(ClaimTypes.Role, role)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("login", "TaiKhoan");
        }
    }
}
