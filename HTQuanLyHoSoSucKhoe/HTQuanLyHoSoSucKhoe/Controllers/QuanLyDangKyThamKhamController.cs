using System.Security.Claims;
using HTQuanLyHoSoSucKhoe.Models;
using HTQuanLyHoSoSucKhoe.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HTQuanLyHoSoSucKhoe.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuanLyDangKyThamKhamController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuanLyDangKyThamKhamController(ApplicationDbContext context)
        {

            _context = context;

        }

        public IActionResult Index()
        {
            // Lấy thông tin người dùng từ claims
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); // Giữ UserId từ claims
            var user = _context.Users.FirstOrDefault(u => u.Id == userId); // Lấy thông tin cá nhân từ database

            // Lấy tất cả các cuộc hẹn của người dùng hiện tại từ cơ sở dữ liệu
            var appointments = _context.Appointments
                .Include(a => a.User)
                .Include(a => a.BenhVien) // Bao gồm thông tin về bệnh viện
                .Include(a => a.LoaiDichVuKham) // Bao gồm thông tin về loại dịch vụ khám
                .Include(a => a.ChuyenKhoa) // Bao gồm thông tin về chuyên khoa
                .ToList();

            // Lấy danh sách các Bệnh viện, Loại dịch vụ, và Chuyên khoa
            var benhVienList = _context.BenhVien.ToList();
            var loaiDichVuList = _context.LoaiDichVuThamKhams.ToList();
            var chuyenKhoaList = _context.ChuyenKhoas.ToList();


            // Chuyển đổi dữ liệu Appointment thành AppointmentViewModel để hiển thị trong View
            var viewModel = appointments.Select(a => new AppointmentViewModel
            {
                Id = a.Id,
                BenhVienId = a.BenhVienId,
                name = a.User.Ho + " " + a.User.Ten, // Tạo tên đầy đủ từ họ và tên
                email = a.User.Email,
                cccd = a.User.Cccd,
                phone_Number = a.User.Phone_Number,
                tenBenhVien = a.BenhVien.Name, // Tên bệnh viện
                Appointment_Date = a.Appointment_Date.Date,
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

        public IActionResult ChiTietPhieuDangKyThamKham(int id)
        {
            // Lấy thông tin người dùng từ claims
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); // Giữ UserId từ claims
            var user = _context.Users.FirstOrDefault(u => u.Id == userId); // Lấy thông tin cá nhân từ database

            // Tìm phiếu thăm khám cụ thể dựa trên Id và UserId
            var appointment = _context.Appointments
                .Where(a => a.Id == id)
                .Include(a => a.User)
                .Include(a => a.BenhVien) // Bao gồm thông tin về bệnh viện
                .Include(a => a.LoaiDichVuKham) // Bao gồm thông tin về loại dịch vụ khám
                .Include(a => a.ChuyenKhoa) // Bao gồm thông tin về chuyên khoa
                .FirstOrDefault();

            var loaiDichVuList = _context.LoaiDichVuThamKhams.ToList();
            var chuyenKhoaList = _context.ChuyenKhoas.ToList();

            if (appointment == null)
            {
                return NotFound("Không tìm thấy phiếu thăm khám.");
            }


            // Tạo ViewModel để truyền dữ liệu sang View
            var viewModel = new AppointmentViewModel
            {
                Id = id,
                BenhVienId = appointment.BenhVienId,
                name = appointment.User.Ho +" "+ appointment.User.Ten,
                email = appointment.User.Email,
                cccd = appointment.User.Cccd,
                phone_Number = appointment.User.Phone_Number,
                tenBenhVien = appointment.BenhVien.Name,
                Appointment_Date = appointment.Appointment_Date.Date,
                trangThai = appointment.trangThaiPhieu,
                LoaiDichVuId = appointment.LoaiDichVuKham.Id,
                soThuTu = appointment.soThuTu,
                TenDichVu = appointment.LoaiDichVuKham.TenDichVu,
                tenChuyenKhoa = appointment.ChuyenKhoa?.Name,

            };

            ViewBag.LoaiDichVuList = loaiDichVuList;
            ViewBag.ChuyenKhoaList = chuyenKhoaList;

            // Trả về View với ViewModel chi tiết
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult chinhSuaThongTin(int appointmentId, AppointmentViewModel model)
        {
            var appointment = _context.Appointments.FirstOrDefault(a => a.Id == appointmentId);
            if (appointment == null)
            {
                return NotFound("Không tìm thấy phiếu thăm khám."); // Nếu không tìm thấy bệnh viện, trả về lỗi
            }

            // Cập nhật các thông tin của cuộc hẹn
            appointment.LoaiDichVuId = model.LoaiDichVuId;
            appointment.ChuyenKhoaId = model.ChuyenKhoaId;
            appointment.Appointment_Date = model.Appointment_Date;

            // Lưu lại thay đổi vào database
            _context.Update(appointment);
            _context.SaveChanges();
            // Thông báo thành công
            TempData["SuccessMessage"] = "Chỉnh sửa thông tin phiếu đăng ký thành công!";
            // Chuyển hướng về trang Index sau khi cập nhật thành công
            return RedirectToAction("ChiTietPhieuDangKyThamKham", new { id = appointmentId });
        }

        [HttpPost]
        public IActionResult chinhSuaTrangThai(int appointmentId, AppointmentViewModel model)
        {
            var appointment = _context.Appointments.FirstOrDefault(a => a.Id == appointmentId);
            if (appointment == null)
            {
                return NotFound("Không tìm thấy phiếu thăm khám."); // Nếu không tìm thấy bệnh viện, trả về lỗi
            }

            appointment.trangThaiPhieu = model.trangThai;

            // Lưu lại thay đổi vào database
            _context.Update(appointment);
            _context.SaveChanges();
            // Thông báo thành công
            TempData["SuccessMessage"] = "Chỉnh sửa trạng thái phiếu đăng ký thành công!";
            // Chuyển hướng về trang Index sau khi cập nhật thành công
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult xoaDangKy(int id)
        {
            // Tìm bản ghi cuộc hẹn cần xóa theo id
            var appointment = _context.Appointments.FirstOrDefault(a => a.Id == id);

            if (appointment == null)
            {
                // Nếu không tìm thấy bản ghi, trả về thông báo lỗi
                return NotFound("Không tìm thấy cuộc hẹn cần xóa.");
            }

            // Xóa bản ghi khỏi database
            _context.Appointments.Remove(appointment);
            _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

            TempData["SuccessMessage"] = "Xóa phiếu đăng ký thành công!";

            // Chuyển hướng về trang danh sách sau khi xóa thành công
            return RedirectToAction("Index");
        }
    }
}
