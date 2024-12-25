using HTQuanLyHoSoSucKhoe.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HTQuanLyHoSoSucKhoe.Models;
using HTQuanLyHoSoSucKhoe.ViewModel;

namespace HTQuanLyHoSoSucKhoe.Controllers
{
    [Authorize(Roles = "CanLamSang")]
    public class QuanLyCanLamSangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuanLyCanLamSangController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var ChuyenKhoaId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID của Chuyên khoa từ claims

            // Kiểm tra nếu không tìm thấy thông tin Chuyên khoa trong Claims
            if (string.IsNullOrEmpty(ChuyenKhoaId) || !int.TryParse(ChuyenKhoaId, out var parsedChuyenKhoaId))
            {
                return RedirectToAction("Login", "TaiKhoan"); // Điều hướng về trang login nếu không hợp lệ
            }
            var phieuKetQuaList = _context.PhieuKetQuas
                .Where(p => p.chuyenKhoaId == parsedChuyenKhoaId)
                .Include(p => p.ChuyenKhoa)
                .Include(p => p.Appointment) 
                .Include (p => p.HoSoBenhAn)
                .Include(p => p.BacSi)
                .Include(p => p.PhieuChiDinh)
                .ThenInclude(pc => pc.ChuyenKhoa)
                .Select(p => new PhieuKetQuaViewModel
                {
                    Id = p.Id,
                    TenChuyenKhoaCanLamSang = _context.ChuyenKhoas
                    .Where(ck => ck.Id == p.chuyenKhoaId)
                    .Select(ck => ck.Name)
                    .FirstOrDefault(), // Tên cận lâm sàng
                            TenChuyenKhoaLamSang = _context.ChuyenKhoas
                    .Where(ck => ck.Id == p.PhieuChiDinh.chuyenKhoaId)
                    .Select(ck => ck.Name)
                    .FirstOrDefault(), // Tên lâm sàng
                    chuyenKhoaId = parsedChuyenKhoaId,
                    appointmentId = p.Appointment.Id,
                    hoSoBenhAnId = p.hoSoBenhAnId,
                    bacSiId = p.bacSiId,
                    phieuChiDinhId = p.phieuChiDinhId,
                    File1 = Url.Content("~/pdf/PhieuKetQua/" + p.duongDanFile1),
                    File2 = Url.Content("~/pdf/PhieuKetQua/" + p.duongDanFile2),
                    File3 = Url.Content("~/pdf/PhieuKetQua/" + p.duongDanFile3),
                })
                .ToList();

            return View(phieuKetQuaList);

            
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
        public IActionResult chinhSuaThongTin(int chuyenKhoaId, ThongTinChuyenKhoaViewModel model)
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

        public IActionResult DanhSachPhieuChiDinh()
        {
            // Lấy chuyenKhoaId từ tài khoản đăng nhập
            var chuyenKhoaId = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int parsedChuyenKhoaId);

            // Lấy danh sách phiếu chỉ định và các thông tin liên quan
            var phieuChiDinhList = _context.PhieuChiDinhs
                .Where(p => p.LoaiChiDinh == parsedChuyenKhoaId)
                .Include(p => p.ChuyenKhoa)
                .Include(p => p.Appointment) // Bao gồm Appointments
                .Select(p => new PhieuChiDinhViewModels
                {
                    Id = p.Id,
                    LoaiChiDinh = _context.ChuyenKhoas
                    .Where(ck => ck.Id == p.LoaiChiDinh) // Tìm tên chuyên khoa dựa trên LoaiChiDinh (ID)
                    .Select(ck => ck.Name)
                    .FirstOrDefault(),
                    chuyenKhoaId = parsedChuyenKhoaId,
                    appointmentId = p.Appointment.Id,
                    Created_At = p.Created_At,
                })
                .ToList();
            var bacSiList = _context.BacSis.Where(i => i.ChuyenKhoaId == parsedChuyenKhoaId).ToList();
            ViewBag.BacSiList = bacSiList;
            return View(phieuChiDinhList);
        }

        [HttpPost]
        public async Task<IActionResult> taoPhieuKetQua(IFormFile file1, IFormFile file2, PhieuChiDinhViewModels viewModel)
        {
            var phieuKetQua = new PhieuKetQua
            {
                phieuChiDinhId = viewModel.Id,
                hoSoBenhAnId = viewModel.hoSoBenhAnId,
                chuyenKhoaId = viewModel.chuyenKhoaId,
                appointmentId = viewModel.appointmentId,
                bacSiId = viewModel.SelectedBacSiId,
                Created_At = DateTime.Now,
            };

            // Kiểm tra và xử lý file đầu tiên
            if (file1 != null)
            {
                var filePath1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdf/PhieuKetQua", file1.FileName);

                // Lưu file vào thư mục
                using (var stream = new FileStream(filePath1, FileMode.Create))
                {
                    await file1.CopyToAsync(stream);
                }

                // Gán đường dẫn file vào thuộc tính của đối tượng
                phieuKetQua.duongDanFile1 = file1.FileName;
            }

            // Kiểm tra và xử lý file thứ hai
            if (file2 != null)
            {
                var filePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdf/PhieuKetQua", file2.FileName);

                // Lưu file vào thư mục
                using (var stream = new FileStream(filePath2, FileMode.Create))
                {
                    await file2.CopyToAsync(stream);
                }

                // Gán đường dẫn file vào thuộc tính của đối tượng
                phieuKetQua.duongDanFile2 = file2.FileName;
            }
            // Lưu thông tin phiếu kết quả vào cơ sở dữ liệu
            _context.PhieuKetQuas.Add(phieuKetQua);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index"); // Chuyển hướng về trang danh sách phiếu kết quả
        }



        [HttpPost]
        public IActionResult XoaPhieuKetQua(int id)
        {
            // Tìm bản ghi cuộc hẹn cần xóa theo id
            var phieuKetQua = _context.PhieuKetQuas.FirstOrDefault(a => a.Id == id);

            if (phieuKetQua == null)
            {
                // Nếu không tìm thấy bản ghi, trả về thông báo lỗi
                return NotFound("Không tìm thấy cuộc hẹn cần xóa.");
            }

            // Xóa bản ghi khỏi database
            _context.PhieuKetQuas.Remove(phieuKetQua);
            _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

            TempData["SuccessMessage"] = "Xóa phiếu kết quả thành công!";

            // Chuyển hướng về trang danh sách sau khi xóa thành công
            return RedirectToAction("Index");
        }

        public IActionResult ChiTietPhieuKetQua(int id)
        {
            var ChuyenKhoaId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID của Chuyên khoa từ claims

            // Kiểm tra nếu không tìm thấy thông tin Chuyên khoa trong Claims
            if (string.IsNullOrEmpty(ChuyenKhoaId) || !int.TryParse(ChuyenKhoaId, out var parsedChuyenKhoaId))
            {
                return RedirectToAction("Login", "TaiKhoan"); // Điều hướng về trang login nếu không hợp lệ
            }
            var phieuKetQua = _context.PhieuKetQuas
                .Where(p => p.chuyenKhoaId == parsedChuyenKhoaId && p.Id == id)
                .Include(p => p.ChuyenKhoa)
                .Include(p => p.Appointment)
                .ThenInclude(a => a.User) // Bao gồm thông tin người dùng
                .Include(p => p.HoSoBenhAn)
                .Include(p => p.BacSi)
                .Include(p => p.PhieuChiDinh)
                .ThenInclude(pc => pc.ChuyenKhoa)
                .Select(p => new PhieuKetQuaViewModel
                {
                    Id = p.Id,
                    TenChuyenKhoaCanLamSang = _context.ChuyenKhoas
                    .Where(ck => ck.Id == p.chuyenKhoaId)
                    .Select(ck => ck.Name)
                    .FirstOrDefault(), // Tên cận lâm sàng
                    TenChuyenKhoaLamSang = _context.ChuyenKhoas
                    .Where(ck => ck.Id == p.PhieuChiDinh.chuyenKhoaId)
                    .Select(ck => ck.Name)
                    .FirstOrDefault(), // Tên lâm sàng
                    chuyenKhoaId = parsedChuyenKhoaId,
                    appointmentId = p.Appointment.Id,
                    hoSoBenhAnId = p.hoSoBenhAnId,
                    bacSiId = p.bacSiId,
                    tenBacSi = _context.BacSis
                    .Where(ck => ck.Id == p.bacSiId)
                    .Select(ck => ck.hoTen)
                    .FirstOrDefault(),
                    phieuChiDinhId = p.phieuChiDinhId,
                    File1 = Url.Content("~/pdf/PhieuKetQua/" + p.duongDanFile1),
                    tenBenhNhan = $"{p.Appointment.User.Ho} {p.Appointment.User.Ten}",
                    PhoneNumber = p.Appointment.User.Phone_Number,
                    Cccd = p.Appointment.User.Cccd,
                    NgayDangKy = p.Appointment.NgayTao,
                    ThoiGianTaoPhieuChiDinh = p.PhieuChiDinh.Created_At
                })
                .FirstOrDefault(); // Lấy đối tượng đầu tiên hoặc null nếu không có

            // Kiểm tra nếu không tìm thấy phiếu kết quả
            if (phieuKetQua == null)
            {
                return NotFound("Phiếu kết quả không tồn tại hoặc bạn không có quyền truy cập.");
            }

            return View(phieuKetQua);

        }

        [HttpPost]
        public async Task<IActionResult> ChinhSuaPhieuKetQua(IFormFile file1, int phieuKetQuaId, PhieuKetQuaViewModel model)
        {

            var ChuyenKhoaId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID của Chuyên khoa từ claims

            // Kiểm tra nếu không tìm thấy thông tin Chuyên khoa trong Claims
            if (string.IsNullOrEmpty(ChuyenKhoaId) || !int.TryParse(ChuyenKhoaId, out var parsedChuyenKhoaId))
            {
                return RedirectToAction("Login", "TaiKhoan"); // Điều hướng về trang login nếu không hợp lệ
            }

            // Lấy phiếu chỉ định từ database
            var phieuKetQua = _context.PhieuKetQuas.FirstOrDefault(p => p.Id == phieuKetQuaId);
            if (phieuKetQua == null)
            {
                return NotFound();
            }

            // Cập nhật thông tin phiếu chỉ định
            if (file1 != null)
            {
                var filePath1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdf/PhieuKetQua", file1.FileName);

                // Lưu file vào thư mục
                using (var stream = new FileStream(filePath1, FileMode.Create))
                {
                    await file1.CopyToAsync(stream);
                }

                // Gán đường dẫn file vào thuộc tính của đối tượng
                phieuKetQua.duongDanFile1 = file1.FileName;
            }


            // Lưu thay đổi vào database
            _context.Update(phieuKetQua);
            _context.SaveChanges();

            // Quay về trang danh sách hoặc thông báo thành công
            return RedirectToAction("ChiTietPhieuKetQua", new { id = phieuKetQuaId });
        }

        public IActionResult ChiTietPhieuChiDinh(int id)
        {
            var ChuyenKhoaId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID của Chuyên khoa từ claims

            // Kiểm tra nếu không tìm thấy thông tin Chuyên khoa trong Claims
            if (string.IsNullOrEmpty(ChuyenKhoaId) || !int.TryParse(ChuyenKhoaId, out var parsedChuyenKhoaId))
            {
                return RedirectToAction("Login", "TaiKhoan"); // Điều hướng về trang login nếu không hợp lệ
            }
            var phieuChiDinh = _context.PhieuChiDinhs
                .Where(p => p.LoaiChiDinh == parsedChuyenKhoaId && p.Id == id)
                .Include(p => p.ChuyenKhoa)
                .Include(p => p.Appointment)
                .ThenInclude(a => a.User) // Bao gồm thông tin người dùng
                .Include(p => p.HoSoBenhAn)
                .ThenInclude(pc => pc.ChuyenKhoa)
                .Select(p => new PhieuChiDinhViewModels
                {
                    Id = p.Id,
                    LoaiChiDinh = _context.ChuyenKhoas
                    .Where(ck => ck.Id == p.LoaiChiDinh) // Tìm tên chuyên khoa dựa trên LoaiChiDinh (ID)
                    .Select(ck => ck.Name)
                    .FirstOrDefault(),
                    ChuyenKhoaName = _context.ChuyenKhoas
                    .Where(ck => ck.Id == p.chuyenKhoaId)
                    .Select(ck => ck.Name)
                    .FirstOrDefault(), // Tên cận lâm sàng
                    tenBacSi = _context.BacSis
                    .Where(ck => ck.Id == p.bacSiId)
                    .Select(ck => ck.hoTen)
                    .FirstOrDefault(),
                    appointmentId = p.Appointment.Id,
                    tenBenhNhan = $"{p.Appointment.User.Ho} {p.Appointment.User.Ten}",
                    PhoneNumber = p.Appointment.User.Phone_Number,
                    Cccd = p.Appointment.User.Cccd,
                    Created_At = p.Created_At,
                    NgayDangKy = p.Appointment.NgayTao,
                }).FirstOrDefault();


            // Kiểm tra nếu không tìm thấy phiếu kết quả
            if (phieuChiDinh == null)
            {
                return NotFound("Phiếu kết quả không tồn tại hoặc bạn không có quyền truy cập.");
            }

            return View(phieuChiDinh);

        }
    }
}
