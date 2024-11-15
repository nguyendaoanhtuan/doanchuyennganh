using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using BCrypt.Net;
using HTQuanLyHoSoSucKhoe.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace HTQuanLyHoSoSucKhoe.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context) // Sửa lại để dùng constructor
        {
            _context = context; // Gán giá trị context cho biến _context
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
        public async Task<IActionResult> Register(User model)
        {
           
            

                // Kiểm tra mật khẩu và xác nhận mật khẩu
                if (model.password != model.confirmpassword)
                {
                    ModelState.AddModelError("", "Mật khẩu và xác nhận mật khẩu không khớp.");
                    return View(model);
                }

                // Kiểm tra số điện thoại đã tồn tại chưa
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Phone_Number == model.Phone_Number);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Số điện thoại đã được sử dụng.");
                    return View(model);
                }

                model.password = BCrypt.Net.BCrypt.HashPassword(model.password);

                // Tạo người dùng mới
                var user = new User
                {
                    Phone_Number = model.Phone_Number,
                    Email = model.Email,
                    Cccd = model.Cccd,
                    Ho = model.Ho,
                    Ten = model.Ten,
                    Address = model.Address,
                    password = model.password  // Chuyển đổi mật khẩu thành PasswordHash
                    
                };

                // Lưu vào cơ sở dữ liệu
                 user.confirmpassword = null; // Chắc chắn rằng không lưu giá trị này
   
                _context.Users.Add(user);
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
        public async Task<IActionResult> LoginAsync(string phone_Number, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Phone_Number == phone_Number);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Ho + " " + user.Ten),
                    new Claim("ho", user.Ho),
                    new Claim("ten", user.Ten)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "login");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Số điện thoại hoặc mật khẩu không đúng.");
            return View();
        }


         public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
