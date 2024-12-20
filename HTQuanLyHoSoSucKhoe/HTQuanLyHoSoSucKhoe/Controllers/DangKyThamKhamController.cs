using HTQuanLyHoSoSucKhoe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;
using HTQuanLyHoSoSucKhoe.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;




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
            // Lấy thông tin người dùng từ claims
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var fullName = User.FindFirstValue(ClaimTypes.Name);
            var email = User.FindFirstValue(ClaimTypes.Email);
            var phoneNumber = User.FindFirstValue("PhoneNumber");
            var cccd = User.FindFirstValue("Cccd");
            // Lấy tất cả các cuộc hẹn của người dùng hiện tại từ cơ sở dữ liệu
            var appointments = _context.Appointments
                .Where(a => a.UserId == userId) // Lọc theo UserId của người dùng đăng nhập
                .Include(a => a.BenhVien) // Bao gồm thông tin về bệnh viện
                .Include(a => a.LoaiDichVuKham) // Bao gồm thông tin về loại dịch vụ khám
                .Include(a => a.ChuyenKhoa) // Bao gồm thông tin về chuyên khoa
                .ToList();

            // Lấy danh sách các Bệnh viện, Loại dịch vụ, và Chuyên khoa
            var benhVienList = _context.BenhVien.ToList();
            var loaiDichVuList = _context.LoaiDichVuThamKhams.ToList();
            var chuyenKhoaList = _context.ChuyenKhoas.ToList();

            // Chuyển đổi dữ liệu người dùng thành đối tượng ViewBag
            ViewBag.UserInfo = new
            {
                Name = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                CCCD = cccd
            };

            // Chuyển đổi dữ liệu Appointment thành AppointmentViewModel để hiển thị trong View
            var viewModel = appointments.Select(a => new AppointmentViewModel
            {
                BenhVienId = a.BenhVienId,
                name = fullName, // Tạo tên đầy đủ từ họ và tên
                email = email,
                cccd = cccd,
                phone_Number = phoneNumber,
                tenBenhVien = a.BenhVien.Name, // Tên bệnh viện
                Appointment_Date = a.Appointment_Date,
                Appointment_Time = a.Appointment_Date.TimeOfDay, // Lấy giờ từ ngày giờ hẹn
                trangThai = a.trangThaiPhieu, // Trạng thái thăm khám, mặc định là "Đang thăm khám"
                LoaiDichVuId = a.LoaiDichVuKham.Id,
                soThuTu = a.soThuTu,
                TenDichVu = a.LoaiDichVuKham.TenDichVu,
            }).ToList();

            ViewBag.BenhVienList = benhVienList;
            ViewBag.LoaiDichVuList = loaiDichVuList;
            ViewBag.ChuyenKhoaList = chuyenKhoaList;

            // Trả về View và truyền ViewModel đã được xử lý
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new AppointmentViewModel
            {
                LoaiDichVuList = _context.LoaiDichVuThamKhams
                    .Select(ldv => new SelectListItem
                    {
                        Value = ldv.Id.ToString(),
                        Text = ldv.TenDichVu
                    }).ToList(),

                BenhVienList = _context.BenhVien
                    .Select(bv => new SelectListItem
                    {
                        Value = bv.Id.ToString(),
                        Text = bv.Name
                    }).ToList()
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult taoPhieuDangKyThamKham(AppointmentViewModel model)
        {
            // Lấy UserId từ claim (tương tự như trong caiDatTaiKhoan)
            var userIdFromClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Kiểm tra xem UserId có tồn tại trong claim không
            if (string.IsNullOrEmpty(userIdFromClaim) || !int.TryParse(userIdFromClaim, out var parseduserId))
            {
                ModelState.AddModelError(string.Empty, "Người dùng không tồn tại hoặc chưa đăng nhập.");
                return RedirectToAction("Login", "TaiKhoan"); // Nếu không có UserId hoặc không thể chuyển đổi, điều hướng về trang đăng nhập
            }
            // Tìm người dùng trong cơ sở dữ liệu bằng UserId từ claim
            var user = _context.Users.FirstOrDefault(u => u.Id.ToString() == userIdFromClaim);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Người dùng không tồn tại.");
                return RedirectToAction("Index"); // Nếu không tìm thấy người dùng, trả về trang chủ
            }

            // Lấy số thứ tự cho buổi khám
            int soThuTu = _context.Appointments
                .Where(a => a.Appointment_Date.Date == model.Appointment_Date.Date)
                .Count() + 1;

            var appointment = new Appointment
            {
                UserId = parseduserId,
                BenhVienId = model.BenhVienId,
                LoaiDichVuId = model.LoaiDichVuId,
                ChuyenKhoaId = model.ChuyenKhoaId,
                Appointment_Date = model.Appointment_Date,
                trangThaiPhieu = "Đang chờ xử lý",
                soThuTu = soThuTu,
            };

            // Thêm buổi khám vào cơ sở dữ liệu
            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            // Chuyển hướng đến trang Index sau khi thành công
            return RedirectToAction("Index");
        }
    

     
      
    }
    
}
