using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using BCrypt.Net;
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
    
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context) // Sửa lại để dùng constructor
        {
            _context = context; // Gán giá trị context cho biến _contexts
        }
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // Kiểm tra mật khẩu và xác nhận mật khẩu
            if (model.password != model.confirmpassword)
            {
                ModelState.AddModelError("", "Mật khẩu và xác nhận mật khẩu không khớp.");
                return View(model);
            }

            // Kiểm tra độ dài số điện thoại
            if (model.Phone_Number.Length < 10)
            {
                ModelState.AddModelError("", "Số điện thoại phải có ít nhất 10 số.");
                return View(model);
            }

            // Kiểm tra độ dài mật khẩu
            if (model.password.Length < 8)
            {
                ModelState.AddModelError("", "Mật khẩu phải có ít nhất 8 ký tự.");
                return View(model);
            }

            // Kiểm tra độ dài CCCD
            if (model.Cccd.Length != 12 || !model.Cccd.All(char.IsDigit))
            {
                ModelState.AddModelError("", "CCCD phải có đúng 12 ký tự số.");
                return View(model);
            }

            // Kiểm tra định dạng email
            if (!Regex.IsMatch(model.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                ModelState.AddModelError("", "Email không hợp lệ.");
                return View(model);
            }

            // Kiểm tra số điện thoại đã tồn tại chưa
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Phone_Number == model.Phone_Number);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Số điện thoại đã được sử dụng.");
                return View(model);
            }

            // Kiểm tra email đã tồn tại chưa
            var existingEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (existingEmail != null)
            {
                ModelState.AddModelError("", "Email đã được sử dụng.");
                return View(model);
            }

            // Kiểm tra CCCD đã tồn tại chưa
            var existingCccd = await _context.Users.FirstOrDefaultAsync(u => u.Cccd == model.Cccd);
            if (existingCccd != null)
            {
                ModelState.AddModelError("", "CCCD đã được sử dụng.");
                return View(model);
            }

            model.RoleId = 1;

            var role = await _context.Roles.FindAsync(model.RoleId);
            if (role == null)
            {
                ModelState.AddModelError("", "Vai trò không tồn tại.");
                return View(model);
            }

            // Lưu thông tin người dùng vào bảng User (không có password ở đây)
            var user = new User
            {
                Phone_Number = model.Phone_Number,
                Email = model.Email,
                Cccd = model.Cccd,
                Ho = model.Ho,
                Ten = model.Ten,
                Address = model.Address,
                RoleId = model.RoleId
            };

            // Lưu mật khẩu vào bảng TaiKhoan (bảng này lưu passWord)
            var taiKhoan = new TaiKhoan
            {
                passWord = BCrypt.Net.BCrypt.HashPassword(model.password), // Mã hóa mật khẩu
                User = user
            };

            // Lưu vào cơ sở dữ liệu
            _context.Users.Add(user);
            _context.TaiKhoans.Add(taiKhoan);
            await _context.SaveChangesAsync();

            // Redirect hoặc trả về thông báo thành công
            return RedirectToAction("Login");
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

        //dành cho bệnh viện để lấy thông tin vào claim
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
            return RedirectToAction("Index", "Home");
        }
    }
}
