using HTQuanLyHoSoSucKhoe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;
using HTQuanLyHoSoSucKhoe.Models;
using HTQuanLyHoSoSucKhoe.ViewModels;



namespace HTQuanLyHoSoSucKhoe.Controllers
{

    [Authorize(Roles = "User")]
    public class DangKyThamKhamController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public DangKyThamKhamController(ApplicationDbContext context)
        {

            _context = context;
            
        }

        public IActionResult Index()
        {

            // Lấy thông tin người dùng đã đăng nhập
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound("User not found.");
            }
            int userIdInt = int.Parse(userId);
            var user = _context.Users.FirstOrDefault(u => u.Id == userIdInt);
         
            ViewBag.BenhVienList = _context.BenhVien.ToList();
            if (user != null)
            {
              
                var model = new Appointment
                {
                    Name = $"{user.Ho} {user.Ten}",
                    Email = user.Email,
                    Phone_Number = user.Phone_Number
                };
                return View(model);
            }
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken] // Bảo vệ chống CSRF
        public async Task<IActionResult> Create(AppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return NotFound("User not found.");
                }
                int userIdInt = int.Parse(userId);
                // Tạo đối tượng Appointment từ dữ liệu trong model
                var appointment = new Appointment
                {
                    UserId = userIdInt, // Lấy ID của người dùng từ form (hoặc thông qua session)
                    BenhVienId = model.BenhVienId, // Lấy ID bệnh viện từ form
                    Name = model.Name,
                    Email = model.Email,
                    Phone_Number = model.Phone_Number,
                    Appointment_Date = model.Appointment_Date,
                };

                // Thêm vào cơ sở dữ liệu
                _context.Appointments.Add(appointment); // _context là ApplicationDbContext

                await _context.SaveChangesAsync(); // Lưu thay đổi vào database

                return RedirectToAction("Index","HoSoSucKhoe"); // Chuyển hướng về trang danh sách hoặc trang thành công
            }
            ViewBag.BenhVienList = _context.BenhVien.ToList();
            return RedirectToAction("Index","Home"); // Nếu ModelState không hợp lệ, quay lại view để hiển thị lỗi
        }
    }
}
