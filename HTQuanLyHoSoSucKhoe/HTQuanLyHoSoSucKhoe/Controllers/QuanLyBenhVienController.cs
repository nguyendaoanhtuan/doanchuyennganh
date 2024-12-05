using HTQuanLyHoSoSucKhoe.Models;
using HTQuanLyHoSoSucKhoe.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HTQuanLyHoSoSucKhoe.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuanLyBenhVienController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuanLyBenhVienController(ApplicationDbContext context)
        {
            _context = context;
        }
 
           public IActionResult Index()
        {
            List<HoSoBenhAnViewModel> models = new List<HoSoBenhAnViewModel>();

            // Lấy tất cả hồ sơ bệnh án từ cơ sở dữ liệu
            var hoSoBenhAns = _context.HoSoBenhAns.Include(h => h.User).ToList();

            foreach (var hoSo in hoSoBenhAns)
            {
                models.Add(new HoSoBenhAnViewModel
                {
                    UserId = hoSo.UserId,
                    UserName = hoSo.User.Ho + " " + hoSo.User.Ten,
                    Cccd = hoSo.User.Cccd,
                    PhoneNumber = hoSo.User.Phone_Number,
                    Email = hoSo.User.Email,
                    BenhVienId = hoSo.BenhVienId,
                    TrieuChung = hoSo.trieuChung,
                    ChanDoan = hoSo.chanDoan,
                    ThuocDuocKe = hoSo.thuocDuocKe,
                    GhiChu = hoSo.GhiChu,
                    HinhAnh = hoSo.hinhAnh,
                    NgayTao = hoSo.ngayTao
                });
            }

            return View(models);  // Truyền danh sách các hồ sơ bệnh án đã lấy
        }
        



        // POST: QuanLyBenhVien/CreateHoSoBenhAn
        [HttpPost]
        public async Task<IActionResult> CreateHoSoBenhAn(HoSoBenhAnViewModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Cccd == model.Cccd);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy tài khoản với CCCD này!";
                return RedirectToAction("Index");
            }
            // Tạo hồ sơ bệnh án từ model truyền vào
            var hoSoBenhAn = new HoSoBenhAn
                {
                    UserId = user.Id,
                    BenhVienId = model.BenhVienId,
                    trieuChung = model.TrieuChung,
                    chanDoan = model.ChanDoan,
                    thuocDuocKe = model.ThuocDuocKe,
                    GhiChu = model.GhiChu,
                    hinhAnh = model.HinhAnh,
                    ngayTao = DateTime.Now,
                    ngayCapNhat = DateTime.Now
                };

                // Lưu hồ sơ bệnh án vào cơ sở dữ liệu
                _context.HoSoBenhAns.Add(hoSoBenhAn);
                await _context.SaveChangesAsync();


            // Lưu UserId vào TempData để truyền sang trang Index
            TempData["UserId"] = user.Id;
            // Thông báo thành công
            TempData["SuccessMessage"] = "Hồ sơ bệnh án đã được tạo thành công!";


            // Quay lại trang Index sau khi lưu thành công và truyền UserId để lấy thông tin người dùng
            return RedirectToAction("Index");
        }
        



        public IActionResult quanLyDatKham()
        {
            // Truy vấn dữ liệu từ database
            var appointments = _context.Appointments
                .Include(a => a.User) 
                .Include(a => a.BenhVien) 
                .Select(a => new
                {
                    Name = a.Name ?? "Không có tên",
                    PhoneNumber = a.Phone_Number ?? "Không có số điện thoại",
                    Email = a.Email ?? "Không có email",
                    AppointmentDate = a.Appointment_Date,
                })
                .ToList();
            return View(appointments);
        }

        public IActionResult chiTietQuanLyDatKham()
        {
            return View();
        }

        public IActionResult chiTietQuanLyHoSoBenhNhan()
        {
            return View();
        }

        public IActionResult caiDatTaiKhoan()
        {
            return View();
        }
    }
}
