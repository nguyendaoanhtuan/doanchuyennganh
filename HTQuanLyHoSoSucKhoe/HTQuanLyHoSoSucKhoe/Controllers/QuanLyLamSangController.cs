using HTQuanLyHoSoSucKhoe.Models;
using HTQuanLyHoSoSucKhoe.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HTQuanLyHoSoSucKhoe.Controllers
{
    [Authorize(Roles = "LamSang")]
    public class QuanLyLamSangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuanLyLamSangController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CaiDatTaiKhoan()
        {
            var chuyenKhoaId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID của bệnh viện từ claims

            // Kiểm tra xem chuyenKhoaId có tồn tại trong claims không
            if (string.IsNullOrEmpty(chuyenKhoaId))
            {
                return RedirectToAction("Login", "TaiKhoan"); // Nếu không có benhVienId, điều hướng về trang login
            }

            // Tìm tài khoản bệnh viện hiện tại dựa trên benhVienId
            var taiKhoan = _context.TaiKhoans.Include(t => t.ChuyenKhoa) // Include BenhVien để lấy thông tin bệnh viện
                                               .FirstOrDefault(t => t.ChuyenKhoa.Id.ToString() == chuyenKhoaId);

            if (taiKhoan == null)
            {
                return NotFound(); // Nếu không tìm thấy tài khoản bệnh viện, trả về lỗi
            }

            var chuyenKhoa = taiKhoan.ChuyenKhoa;  // Lấy thông tin bệnh viện từ tài khoản

            // Tạo ViewModel và gán dữ liệu từ ChuyenKhoa
            var viewModel = new ThongTinChuyenKhoaViewModel
            {
                tenChuyenKhoa = chuyenKhoa.Name,
                soTaiKhoan = chuyenKhoa.phoneNumber,
                matKhau = taiKhoan.passWord,
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult chinhSuaThongTin(int chuyenKhoaId ,ThongTinChuyenKhoaViewModel model)
        {
            var benhVienId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID của bệnh viện từ claims

            // Kiểm tra nếu không tìm thấy thông tin bệnh viện trong Claims
            if (string.IsNullOrEmpty(benhVienId) || !int.TryParse(benhVienId, out var parsedBenhVienId))
            {
                return RedirectToAction("Login", "TaiKhoan"); // Điều hướng về trang login nếu không hợp lệ
            }

            // Tìm tài khoản bệnh viện hiện tại dựa trên benhVienId
            var taiKhoan = _context.TaiKhoans.Include(t => t.ChuyenKhoa) // Include BenhVien để lấy thông tin bệnh viện
                                              .FirstOrDefault(t => t.ChuyenKhoa.Id == parsedBenhVienId);

            if (taiKhoan == null)
            {
                return NotFound(); // Nếu không tìm thấy tài khoản bệnh viện, trả về lỗi
            }



            // Lấy bệnh viện của tài khoản bệnh viện hiện tại
            var chuyenKhoa = _context.ChuyenKhoas.FirstOrDefault(ck => ck.Id == chuyenKhoaId && ck.BenhVienId == parsedBenhVienId);
            if (chuyenKhoa == null)
            {
                return NotFound(); // Nếu không tìm thấy chuyên khoa ,bệnh viện, trả về lỗi
            }


            // Cập nhật thông tin chuyên khoa
            chuyenKhoa.phoneNumber = model.soTaiKhoan;


            // Lưu thay đổi vào cơ sở dữ liệu
            _context.SaveChanges();

            return RedirectToAction("caiDatTaiKhoan"); // Điều hướng về trang chính sau khi cập nhật thành công

        }

        [HttpPost]
        public async Task<IActionResult> thayDoiMatKhau(ThongTinChuyenKhoaViewModel model)
        {

            var benhVienId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID bệnh viện từ claims

            // Kiểm tra nếu không tìm thấy thông tin bệnh viện trong Claims
            if (string.IsNullOrEmpty(benhVienId) || !int.TryParse(benhVienId, out var parsedBenhVienId))
            {
                return RedirectToAction("Login", "TaiKhoan"); // Điều hướng về trang login nếu không hợp lệ
            }

            // Lấy tài khoản bệnh viện
            var taiKhoan = await _context.TaiKhoans.Include(t => t.BenhVien)
                                                     .FirstOrDefaultAsync(t => t.BenhVien.Id == parsedBenhVienId);

            if (taiKhoan == null)
            {
                return NotFound(); // Nếu không tìm thấy tài khoản, trả về lỗi
            }

            // Kiểm tra mật khẩu cũ
            if (taiKhoan.passWord != model.matKhauCu)
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

            // Cập nhật mật khẩu mới
            taiKhoan.passWord = model.matKhauMoi;
            _context.Update(taiKhoan);
            await _context.SaveChangesAsync();

            return RedirectToAction("caiDatTaiKhoan"); // Điều hướng về trang chính sau khi cập nhật
        }
    }
}
