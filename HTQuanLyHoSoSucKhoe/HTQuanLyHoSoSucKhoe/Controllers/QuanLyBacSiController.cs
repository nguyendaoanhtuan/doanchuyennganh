using System.Security.Claims;
using HTQuanLyHoSoSucKhoe.Models;
using HTQuanLyHoSoSucKhoe.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HTQuanLyHoSoSucKhoe.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuanLyBacSiController : Controller
    {

        private readonly ApplicationDbContext _context;

        public QuanLyBacSiController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.ChuyenKhoaList = _context.ChuyenKhoas.ToList();
            var bacSiList = _context.BacSis.Select(b => new ThongTinBacSiViewModel
            {
                bacSiId = b.Id,
                hoTen = b.hoTen,
                ngaySinh = b.ngaySinh,
                gioiTinh = b.gioiTinh,
                soDienThoai = b.soDienThoai,
                email = b.email,
                diaChi = b.diaChi,
                ChuyenKhoaId = b.ChuyenKhoa.Id,
                tenChuyenKhoa = b.ChuyenKhoa.Name,
                BenhVienId = b.BenhVien.Id,
                tenBenhVien = b.BenhVien.Name,
                ngayTaoBacSi = b.ngayTaoBacSi,
                Image_Path = b.Image_Path
            }).ToList();

            // Truyền dữ liệu vào View
            return View(bacSiList);
        }

        public async Task<IActionResult> CreateBacSi(ThongTinBacSiViewModel model)
        {
            var benhVienId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID bệnh viện từ claims

            // Kiểm tra xem benhVienId có tồn tại trong claims không
            if (string.IsNullOrEmpty(benhVienId))
            {
                return RedirectToAction("Login", "Users"); // Nếu không có benhVienId, điều hướng về trang login
            }

            // Chuyển đổi benhVienId thành kiểu int (giả sử Id là kiểu int)
            if (!int.TryParse(benhVienId, out var parsedBenhVienId))
            {
                return RedirectToAction("Login", "Users"); // Nếu không thể chuyển đổi ID, điều hướng về trang login
            }


            model.Image_Path = "Chưa có hình ảnh";

            // Tạo bác sĩ từ model truyền vào
            var bacSi = new BacSi
            {
                hoTen = model.hoTen,
                ngaySinh = model.ngaySinh,
                gioiTinh = model.gioiTinh,
                soDienThoai = model.soDienThoai,
                email = model.email,
                diaChi = model.diaChi,
                ChuyenKhoaId = model.ChuyenKhoaId,
                BenhVienId = parsedBenhVienId, // Lưu ID bệnh viện lấy từ claims vào BacSi
                ngayTaoBacSi = DateTime.Now,
                Image_Path = model.Image_Path
            };

            // Lưu bác sĩ vào cơ sở dữ liệu
            _context.BacSis.Add(bacSi);
            await _context.SaveChangesAsync();

            // Thông báo thành công
            TempData["SuccessMessage"] = "Bác sĩ đã được tạo thành công!";

            // Quay lại trang Index sau khi lưu thành công
            return RedirectToAction("Index");
        }



        public IActionResult ChiTietThongTinBacSi(int id)
        {
            var bacSi = _context.BacSis
               .Where(b => b.Id == id)
               .Select(b => new ThongTinBacSiViewModel
         {
                   bacSiId = b.Id,
                   hoTen = b.hoTen,
                ngaySinh = b.ngaySinh,
                gioiTinh = b.gioiTinh,
                soDienThoai = b.soDienThoai,
                email = b.email,
                diaChi = b.diaChi,
                ChuyenKhoaId = b.ChuyenKhoa.Id,
                tenChuyenKhoa = b.ChuyenKhoa.Name,
                BenhVienId = b.BenhVien.Id,
                tenBenhVien = b.BenhVien.Name,
                ngayTaoBacSi = b.ngayTaoBacSi,
                Image_Path = "/img/Logo_BacSi/" + b.Image_Path
               }).FirstOrDefault();

            // Nếu không tìm thấy bác sĩ, trả về trang lỗi hoặc thông báo
            if (bacSi == null)
            {
                return NotFound("Không tìm thấy thông tin bác sĩ.");
            }

            // Truyền dữ liệu vào View
            return View(bacSi);
        }

    }


}
