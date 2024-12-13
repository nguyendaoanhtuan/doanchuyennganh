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
            // Lấy ID của bệnh viện từ Claims
            var benhVienId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Kiểm tra nếu không tìm thấy thông tin bệnh viện trong Claims
            if (string.IsNullOrEmpty(benhVienId) || !int.TryParse(benhVienId, out var parsedBenhVienId))
            {
                return RedirectToAction("Login", "Users"); // Điều hướng về trang login nếu không hợp lệ
            }
            ViewBag.ChuyenKhoaList = _context.ChuyenKhoas
               .Where(ck => ck.BenhVienId == parsedBenhVienId) // Lọc theo BenhVienId
               .Select(ck => new
               {
                   chuyenKhoaId = ck.Id,
                   tenChuyenKhoa = ck.Name
               })
               .ToList();
            var bacSiList = _context.BacSis
             .Where(b => b.BenhVienId == parsedBenhVienId) 
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
                    Image_Path = b.Image_Path
                }).ToList();

            // Truyền dữ liệu vào View
            return View(bacSiList);
        }

        public async Task<IActionResult> CreateBacSi(ThongTinBacSiViewModel model)
        {
            var benhVienId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID bệnh viện từ claims

            // Kiểm tra nếu không tìm thấy thông tin bệnh viện trong Claims
            if (string.IsNullOrEmpty(benhVienId) || !int.TryParse(benhVienId, out var parsedBenhVienId))
            {
                return RedirectToAction("Login", "Users"); // Điều hướng về trang login nếu không hợp lệ
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
            ViewBag.bacSiId = id;
            // Truyền dữ liệu vào View
            return View(bacSi);
        }

        [HttpPost]
        public async Task<IActionResult> capNhatHinhAnh(int bacSiId, IFormFile ImageFile)
        {
            var benhVienId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID của bệnh viện từ claims

            // Kiểm tra nếu không tìm thấy thông tin bệnh viện trong Claims
            if (string.IsNullOrEmpty(benhVienId) || !int.TryParse(benhVienId, out var parsedBenhVienId))
            {
                return RedirectToAction("Login", "Users"); // Điều hướng về trang login nếu không hợp lệ
            }

            // Tìm tài khoản bệnh viện hiện tại dựa trên benhVienId
            var taiKhoan = _context.TaiKhoans.Include(t => t.BenhVien) // Include BenhVien để lấy thông tin bệnh viện
                                              .FirstOrDefault(t => t.BenhVien.Id == parsedBenhVienId);

            if (taiKhoan == null)
            {
                return NotFound(); // Nếu không tìm thấy tài khoản bệnh viện, trả về lỗi
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Tạo đường dẫn lưu trữ hình ảnh trong thư mục wwwroot/images
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Logo_BacSi", ImageFile.FileName);

                // Lưu hình ảnh vào thư mục
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                // Cập nhật đường dẫn hình ảnh vào cơ sở dữ liệu cho bác sĩ liên quan đến benhVienId
                var bacsi = _context.BacSis.FirstOrDefault(b => b.Id == bacSiId && b.BenhVienId == parsedBenhVienId);

                if (bacsi != null)
                {
                    bacsi.Image_Path = ImageFile.FileName;
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("ChiTietThongTinBacSi", new { id = bacSiId });
        }


        [HttpPost]
        public IActionResult ChinhSuaThongTin(int bacSiId, ThongTinBacSiViewModel model)
        {
            var benhVienId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID của bệnh viện từ claims

            // Kiểm tra nếu không tìm thấy thông tin bệnh viện trong Claims
            if (string.IsNullOrEmpty(benhVienId) || !int.TryParse(benhVienId, out var parsedBenhVienId))
            {
                return RedirectToAction("Login", "Users"); // Điều hướng về trang login nếu không hợp lệ
            }

            // Tìm tài khoản bệnh viện hiện tại dựa trên benhVienId
            var taiKhoan = _context.TaiKhoans.Include(t => t.BenhVien) // Include BenhVien để lấy thông tin bệnh viện
                                              .FirstOrDefault(t => t.BenhVien.Id == parsedBenhVienId);

            if (taiKhoan == null)
            {
                return NotFound(); // Nếu không tìm thấy tài khoản bệnh viện, trả về lỗi
            }

            // Lấy bệnh viện của tài khoản bệnh viện hiện tại
            var bacsi = _context.BacSis.FirstOrDefault(b => b.Id == bacSiId && b.BenhVienId == parsedBenhVienId);
            if (bacsi == null)
            {
                return NotFound(); // Nếu không tìm thấy bệnh viện, trả về lỗi
            }


            // Cập nhật thông tin bệnh viện
            bacsi.hoTen = model.hoTen;
            bacsi.soDienThoai = model.soDienThoai;
            bacsi.email = model.email;
            bacsi.diaChi = model.diaChi;

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.SaveChanges();

            return RedirectToAction("ChiTietThongTinBacSi", new { id = bacSiId }); // Điều hướng về trang chính sau khi cập nhật thành công

        }

        [HttpPost]
        public async Task<IActionResult> XoaThongTin(int bacSiId)
        {
            // Lấy ID bệnh viện từ claims
            var benhVienId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Kiểm tra nếu không tìm thấy thông tin bệnh viện trong Claims
            if (string.IsNullOrEmpty(benhVienId) || !int.TryParse(benhVienId, out var parsedBenhVienId))
            {
                return RedirectToAction("Login", "Users"); // Điều hướng về trang login nếu không hợp lệ
            }

            // Tìm bác sĩ cần xóa
            var bacsi = _context.BacSis.FirstOrDefault(b => b.Id == bacSiId && b.BenhVienId == parsedBenhVienId);


            // Kiểm tra xem bác sĩ có tồn tại không
            if (bacsi == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy bác sĩ để xóa!";
                return RedirectToAction("Index");
            }

            // Xóa bác sĩ khỏi cơ sở dữ liệu
            _context.BacSis.Remove(bacsi);
            await _context.SaveChangesAsync();

            // Thông báo thành công
            TempData["SuccessMessage"] = "bác sĩ đã được xóa thành công!";

            // Quay lại trang Index sau khi xóa thành công
            return RedirectToAction("Index");
        }

    }


}
