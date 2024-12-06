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

            model.Address = "Chưa có thông tin";

            model.Image_Path = "Chưa có hình ảnh";

            // Lưu thông tin người dùng vào bảng User (không có password ở đây)
            var user = new User
            {
                Phone_Number = model.Phone_Number,
                Email = model.Email,
                Cccd = model.Cccd,
                Ho = model.Ho,
                Ten = model.Ten,
                Address = model.Address,
                Image_Path = model.Image_Path,
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

        public IActionResult caiDatTaiKhoan()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID của user từ claims

            // Kiểm tra xem userId có tồn tại trong claims không
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Users"); // Nếu không có userId, điều hướng về trang login
            }

            // Tìm tài khoản bệnh viện hiện tại dựa trên userId
            var taiKhoan = _context.TaiKhoans.Include(t => t.User) // Include user để lấy thông tin user
                                               .FirstOrDefault(t => t.User.Id.ToString() == userId);

            if (taiKhoan == null)
            {
                return NotFound(); // Nếu không tìm thấy tài khoản bệnh viện, trả về lỗi
            }

            var user = taiKhoan.User;  // Lấy thông tin user từ tài khoản

            // Tạo ViewModel và gán dữ liệu từ user
            var viewModel = new ThongTinNguoiDungViewModel
            {
                Ho = user.Ho,
                Ten = user.Ten,
                soDienThoai = user.Phone_Number,
                cCCD = user.Cccd,
                diaChi = user.Address,
                duongDanHinhAnh = "/img/Logo_User/" + user.Image_Path
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> capNhatHinhAnh(IFormFile ImageFile)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID của user từ claims

            // Kiểm tra xem userId có tồn tại trong claims không
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Users"); // Nếu không có userId, điều hướng về trang login
            }

            // Chuyển đổi userId thành kiểu int (giả sử Id là kiểu int)
            if (!int.TryParse(userId, out var parseduserId))
            {
                return RedirectToAction("Login", "Users"); // Nếu không thể chuyển đổi ID, điều hướng về trang login
            }

            // Tìm tài khoản user hiện tại dựa trên userId
            var taiKhoan = _context.TaiKhoans.Include(t => t.User) // Include user để lấy thông tin user
                                              .FirstOrDefault(t => t.User.Id == parseduserId);

            if (taiKhoan == null)
            {
                return NotFound(); // Nếu không tìm thấy tài khoản bệnh viện, trả về lỗi
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Tạo đường dẫn lưu trữ hình ảnh trong thư mục wwwroot/images
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Logo_User", ImageFile.FileName);

                // Lưu hình ảnh vào thư mục
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                // Cập nhật đường dẫn hình ảnh vào cơ sở dữ liệu cho user liên quan đến userId
                var user = _context.Users.FirstOrDefault(bv => bv.Id == parseduserId);
                if (user != null)
                {
                    user.Image_Path = ImageFile.FileName;
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("caiDatTaiKhoan");
        }


        [HttpPost]
        public IActionResult chinhSuaThongTin(ThongTinNguoiDungViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID của bệnh viện từ claims

            // Kiểm tra xem userId có tồn tại trong claims không
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Users"); // Nếu không có userId, điều hướng về trang login
            }

            // Chuyển đổi userId thành kiểu int (giả sử Id là kiểu int)
            if (!int.TryParse(userId, out var parseduserId))
            {
                return RedirectToAction("Login", "Users"); // Nếu không thể chuyển đổi ID, điều hướng về trang login
            }

            // Tìm tài khoản user hiện tại dựa trên userId
            var taiKhoan = _context.TaiKhoans.Include(t => t.User) // Include user để lấy thông tin user
                                              .FirstOrDefault(t => t.User.Id == parseduserId);

            if (taiKhoan == null)
            {
                return NotFound(); // Nếu không tìm thấy tài khoản user, trả về lỗi
            }

            // Lấy user của tài khoản user hiện tại
            var user = _context.Users.FirstOrDefault(bv => bv.Id == parseduserId);
            if (user == null)
            {
                return NotFound(); // Nếu không tìm thấy user, trả về lỗi
            }


            // Cập nhật thông tin user
            user.Ho = model.Ho;
            user.Ten = model.Ten;
            user.Phone_Number = model.soDienThoai;
            user.Cccd = model.cCCD;
            user.Address = model.diaChi;
        

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.SaveChanges();

            return RedirectToAction("caiDatTaiKhoan"); // Điều hướng về trang chính sau khi cập nhật thành công

        }


        [HttpPost]
        public async Task<IActionResult> thayDoiMatKhau(ThongTinNguoiDungViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID user từ claims

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Users"); // Nếu không có userId, điều hướng đến trang login
            }

            // Chuyển đổi userId thành kiểu int
            if (!int.TryParse(userId, out var parsedUserId))
            {
                return RedirectToAction("Login", "Users");
            }

            // Lấy tài khoản user
            var taiKhoan = await _context.TaiKhoans.Include(t => t.User)
                                                   .FirstOrDefaultAsync(t => t.User.Id == parsedUserId);

            if (taiKhoan == null)
            {
                return NotFound(); // Nếu không tìm thấy tài khoản, trả về lỗi
            }

            // Xác thực mật khẩu cũ bằng BCrypt
            if (!BCrypt.Net.BCrypt.Verify(model.matKhauCu, taiKhoan.passWord))
            {
                ModelState.AddModelError("", "Mật khẩu cũ không đúng.");
                return View(model);
            }

            // Kiểm tra mật khẩu mới và mật khẩu nhập lại có khớp không
            if (model.matKhauMoi != model.nhapLaiMatKhau)
            {
                ModelState.AddModelError("", "Mật khẩu mới và mật khẩu nhập lại không khớp.");
                return View(model);
            }

            // Mã hóa mật khẩu mới bằng BCrypt và lưu lại
            taiKhoan.passWord = BCrypt.Net.BCrypt.HashPassword(model.matKhauMoi);
            _context.Update(taiKhoan);
            await _context.SaveChangesAsync();

            return RedirectToAction("caiDatTaiKhoan"); // Điều hướng về trang chính sau khi cập nhật
        }



    }
}
