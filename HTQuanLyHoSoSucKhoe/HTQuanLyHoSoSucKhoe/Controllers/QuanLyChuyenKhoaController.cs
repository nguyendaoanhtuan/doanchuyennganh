using System.Security.Claims;
using HTQuanLyHoSoSucKhoe.Models;
using HTQuanLyHoSoSucKhoe.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HTQuanLyHoSoSucKhoe.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuanLyChuyenKhoaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuanLyChuyenKhoaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: QuanLyChuyenKhoa
        public IActionResult Index()
        {
            var benhVienId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Kiểm tra nếu không tìm thấy thông tin bệnh viện trong Claims
            if (string.IsNullOrEmpty(benhVienId) || !int.TryParse(benhVienId, out var parsedBenhVienId))
            {
                return RedirectToAction("Login", "Users"); // Điều hướng về trang login nếu không hợp lệ
            }
            var chuyenKhoaList = _context.ChuyenKhoas
             .Where(c => c.BenhVienId == parsedBenhVienId)
             .Select(c => new ThongTinChuyenKhoaViewModel
            {
                chuyenKhoaId = c.Id,
                tenChuyenKhoa = c.Name,
                BenhVienId = c.BenhVienId,
                tenBenhVien = c.BenhVien.Name, 
               
            }).ToList();

            return View(chuyenKhoaList);
        }

        // POST: QuanLyChuyenKhoa/Create
        [HttpPost]
        public async Task<IActionResult> CreateChuyenKhoa(ThongTinChuyenKhoaViewModel model)
        {
            // Lấy ID bệnh viện từ claims (tương tự như bạn làm trong phương thức CreateBacSi)
            var benhVienId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID bệnh viện từ claims

            // Kiểm tra nếu không tìm thấy thông tin bệnh viện trong Claims
            if (string.IsNullOrEmpty(benhVienId) || !int.TryParse(benhVienId, out var parsedBenhVienId))
            {
                return RedirectToAction("Login", "Users"); // Điều hướng về trang login nếu không hợp lệ
            }

            // Tạo chuyên khoa từ model truyền vào
            var chuyenKhoa = new ChuyenKhoa
            {
                Name = model.tenChuyenKhoa,
                BenhVienId = parsedBenhVienId, // Lưu ID bệnh viện lấy từ claims vào ChuyenKhoa
               
            };

            // Lưu chuyên khoa vào cơ sở dữ liệu
            _context.ChuyenKhoas.Add(chuyenKhoa);
            await _context.SaveChangesAsync();

            // Thông báo thành công
            TempData["SuccessMessage"] = "Chuyên khoa đã được tạo thành công!";

            // Quay lại trang Index sau khi lưu thành công
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChinhSuaThongTin(int chuyenKhoaId  ,ThongTinChuyenKhoaViewModel model)
        {
            // Lấy ID bệnh viện từ claims
            var benhVienId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Kiểm tra nếu không tìm thấy thông tin bệnh viện trong Claims
            if (string.IsNullOrEmpty(benhVienId) || !int.TryParse(benhVienId, out var parsedBenhVienId))
            {
                return RedirectToAction("Login", "Users"); // Điều hướng về trang login nếu không hợp lệ
            }

            // Kiểm tra xem chuyên khoa có tồn tại không
            var chuyenKhoa = await _context.ChuyenKhoas
                .FirstOrDefaultAsync(c => c.Id == chuyenKhoaId && c.BenhVienId == parsedBenhVienId);

            if (chuyenKhoa == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy chuyên khoa để chỉnh sửa!";
                return RedirectToAction("Index");
            }

            // Cập nhật thông tin chuyên khoa
            chuyenKhoa.Name = model.tenChuyenKhoa;

            _context.ChuyenKhoas.Update(chuyenKhoa);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Chuyên khoa đã được chỉnh sửa thành công!";
            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> XoaThongTin(int chuyenKhoaId)
        {
            // Lấy ID bệnh viện từ claims
            var benhVienId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Kiểm tra nếu không tìm thấy thông tin bệnh viện trong Claims
            if (string.IsNullOrEmpty(benhVienId) || !int.TryParse(benhVienId, out var parsedBenhVienId))
            {
                return RedirectToAction("Login", "Users"); // Điều hướng về trang login nếu không hợp lệ
            }

            // Tìm chuyên khoa cần xóa
            var chuyenKhoa = await _context.ChuyenKhoas
                .FirstOrDefaultAsync(c => c.Id == chuyenKhoaId && c.BenhVienId == parsedBenhVienId);

      
            // Kiểm tra xem chuyên khoa có tồn tại không
            if (chuyenKhoa == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy chuyên khoa để xóa!";
                return RedirectToAction("Index");
            }
       
            // Xóa chuyên khoa khỏi cơ sở dữ liệu
            _context.ChuyenKhoas.Remove(chuyenKhoa);
            await _context.SaveChangesAsync();

            // Thông báo thành công
            TempData["SuccessMessage"] = "Chuyên khoa đã được xóa thành công!";

            // Quay lại trang Index sau khi xóa thành công
            return RedirectToAction("Index");
        }

    }
}
