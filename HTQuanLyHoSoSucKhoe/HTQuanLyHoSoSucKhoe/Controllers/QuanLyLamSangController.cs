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
        public IActionResult CaiDatTaiKhoan()
        {
            var chuyenKhoaId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID của chuyên khoa từ claims

            // Kiểm tra xem chuyenKhoaId có tồn tại trong claims không
            if (string.IsNullOrEmpty(chuyenKhoaId) || !int.TryParse(chuyenKhoaId, out var parsedchuyenKhoaId))
            {
                return RedirectToAction("Login", "TaiKhoan"); // Nếu không có chuyenkhoaId, điều hướng về trang login
            }

            // Tìm tài khoản chuyên khoa hiện tại dựa trên chuyenkhoaId
            var taiKhoan = _context.TaiKhoans.Include(t => t.ChuyenKhoa) // Include ChuyenKhoa để lấy thông tin chuyên khoa
                                               .FirstOrDefault(t => t.ChuyenKhoa.Id == parsedchuyenKhoaId);

            if (taiKhoan == null)
            {
                return NotFound(); // Nếu không tìm thấy tài khoản chuyên khoa, trả về lỗi
            }

            var chuyenKhoa = taiKhoan.ChuyenKhoa;  // Lấy thông tin chuyên khoa từ tài khoản

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
            var ChuyenKhoaId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID của Chuyên khoa từ claims

            // Kiểm tra nếu không tìm thấy thông tin Chuyên khoa trong Claims
            if (string.IsNullOrEmpty(ChuyenKhoaId) || !int.TryParse(ChuyenKhoaId, out var parsedChuyenKhoaId))
            {
                return RedirectToAction("Login", "TaiKhoan"); // Điều hướng về trang login nếu không hợp lệ
            }

            // Tìm tài khoản Chuyên khoa hiện tại dựa trên ChuyenKhoaId
            var taiKhoan = _context.TaiKhoans.Include(t => t.ChuyenKhoa) // Include ChuyenKhoa để lấy thông tin Chuyên khoa
                                              .FirstOrDefault(t => t.ChuyenKhoa.Id == parsedChuyenKhoaId);

            if (taiKhoan == null)
            {
                return NotFound(); // Nếu không tìm thấy tài khoản Chuyên khoa, trả về lỗi
            }



            // Lấy Chuyên khoa của tài khoản Chuyên khoa hiện tại
            var chuyenKhoa = _context.ChuyenKhoas.FirstOrDefault(ck => ck.Id == parsedChuyenKhoaId);
            if (chuyenKhoa == null)
            {
                return NotFound(); // Nếu không tìm thấy chuyên khoa ,bệnh viện, trả về lỗi
            }


            // Cập nhật thông tin chuyên khoa
            chuyenKhoa.phoneNumber = model.soTaiKhoan;


            // Lưu thay đổi vào cơ sở dữ liệu
            _context.SaveChanges();

            return RedirectToAction("CaiDatTaiKhoan"); // Điều hướng về trang chính sau khi cập nhật thành công

        }

        [HttpPost]
        public async Task<IActionResult> thayDoiMatKhau(ThongTinChuyenKhoaViewModel model)
        {

            var chuyenKhoaId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID của chuyên khoa từ claims
            Console.WriteLine($"chuyenKhoaId: {chuyenKhoaId}");
            // Kiểm tra xem chuyenKhoaId có tồn tại trong claims không
            if (string.IsNullOrEmpty(chuyenKhoaId) || !int.TryParse(chuyenKhoaId, out var parsedchuyenKhoaId))
            {
                return RedirectToAction("Login", "TaiKhoan"); // Nếu không có chuyenkhoaId, điều hướng về trang login
            }

            // Tìm tài khoản chuyên khoa hiện tại dựa trên chuyenkhoaId
            var taiKhoan = _context.TaiKhoans.Include(t => t.ChuyenKhoa) // Include ChuyenKhoa để lấy thông tin chuyên khoa
                                               .FirstOrDefault(t => t.ChuyenKhoa.Id == parsedchuyenKhoaId);

            if (taiKhoan == null)
            {
                return NotFound(); // Nếu không tìm thấy tài khoản chuyên khoa, trả về lỗi
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

            return RedirectToAction("CaiDatTaiKhoan"); // Điều hướng về trang chính sau khi cập nhật
        }
    }
}
