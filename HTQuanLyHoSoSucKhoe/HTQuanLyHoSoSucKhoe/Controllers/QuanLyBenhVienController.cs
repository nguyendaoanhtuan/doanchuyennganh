﻿using System.Security.Claims;
using HTQuanLyHoSoSucKhoe.Models;
using HTQuanLyHoSoSucKhoe.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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
            ViewBag.ChuyenKhoaList = _context.ChuyenKhoas.ToList();
            ViewBag.BacSiList = _context.BacSis.Select(d => new { d.Id,d.hoTen, d.ChuyenKhoaId }).ToList(); // Bao gồm SpecialtyId để lọc
            ViewBag.LoaiPhieuList = _context.LoaiPhieus.ToList();
            List<HoSoBenhAnViewModel> models = new List<HoSoBenhAnViewModel>();

            // Lấy tất cả hồ sơ bệnh án từ cơ sở dữ liệu
            var hoSoBenhAns = _context.PhieuKetQuas.Include(h => h.User).ToList();

            foreach (var hoSo in hoSoBenhAns)
            {
                models.Add(new HoSoBenhAnViewModel
                {
                    //UserId = hoSo.UserId,
                   // Cccd = hoSo.User.Cccd,
                    //PhoneNumber = hoSo.User.Phone_Number,
                    //Email = hoSo.User.Email,
                    //BenhVienId = hoSo.BenhVienId,
                    //ThuocDuocKe = hoSo.DonThuoc?? "",
                    PhieuId = hoSo.Id,
                    //DuongDanPhieu = hoSo.DuongDanPhieu,
                   // GhiChu = hoSo.GhiChu ?? "",
                    NgayTao = hoSo.NgayTao,
                    TenBacSi = _context.BacSis.FirstOrDefault(w => w.Id == hoSo.BacSiId).hoTen,
                    TenPhieu = _context.LoaiPhieus.FirstOrDefault(i => i.Id == hoSo.LoaiPhieuId).TenLoai,
                    TenChuyenKhoa = _context.ChuyenKhoas.FirstOrDefault(i => i.Id == _context.BacSis.FirstOrDefault(w => w.Id == hoSo.BacSiId).ChuyenKhoaId).Name,
                }); ;
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
            var benhVienId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID của bệnh viện từ claims

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

            // Tìm tài khoản bệnh viện hiện tại dựa trên benhVienId
            var taiKhoan = _context.TaiKhoans.Include(t => t.BenhVien) // Include BenhVien để lấy thông tin bệnh viện
                                              .FirstOrDefault(t => t.BenhVien.Id == parsedBenhVienId);

            if (taiKhoan == null)
            {
                return NotFound(); // Nếu không tìm thấy tài khoản bệnh viện, trả về lỗi
            }

            // Lấy bệnh viện của tài khoản bệnh viện hiện tại
            var benhVien = _context.BenhVien.FirstOrDefault(bv => bv.Id == parsedBenhVienId);

            // Tạo hồ sơ bệnh án từ model truyền vào
            var hoSoBenhAn = new PhieuKetQua
                {
                    UserId = user.Id,
                    BenhVienId = benhVien.Id,
                    GhiChu = model.GhiChu,
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    LoaiPhieuId = model.LoaiPhieuId,
                    BacSiId = model.BacSiId,
                    DonThuoc = model.ThuocDuocKe,
                    DuongDanPhieu = model.DuongDanPhieu,
                };

                // Lưu hồ sơ bệnh án vào cơ sở dữ liệu
                _context.PhieuKetQuas.Add(hoSoBenhAn);
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

        public IActionResult CaiDatTaiKhoan()
        {
            var benhVienId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID của bệnh viện từ claims

            // Kiểm tra xem benhVienId có tồn tại trong claims không
            if (string.IsNullOrEmpty(benhVienId))
            {
                return RedirectToAction("Login", "Users"); // Nếu không có benhVienId, điều hướng về trang login
            }

            // Tìm tài khoản bệnh viện hiện tại dựa trên benhVienId
            var taiKhoan = _context.TaiKhoans.Include(t => t.BenhVien) // Include BenhVien để lấy thông tin bệnh viện
                                               .FirstOrDefault(t => t.BenhVien.Id.ToString() == benhVienId);

            if (taiKhoan == null)
            {
                return NotFound(); // Nếu không tìm thấy tài khoản bệnh viện, trả về lỗi
            }

            var benhVien = taiKhoan.BenhVien;  // Lấy thông tin bệnh viện từ tài khoản

            // Tạo ViewModel và gán dữ liệu từ BenhVien
            var viewModel = new ThongTinBenhVienViewModel
            {
                TenBenhVien = benhVien.Name,
                SoDienThoai = benhVien.Phone_Number,
                DiaChi = benhVien.Address,
                DuongDanHinhAnh = "/img/Logo_BenhViens/" + benhVien.Image_Path
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> capNhatHinhAnh(IFormFile ImageFile)
        {
            var benhVienId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID của bệnh viện từ claims

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
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Logo_BenhViens", ImageFile.FileName);

                // Lưu hình ảnh vào thư mục
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                // Cập nhật đường dẫn hình ảnh vào cơ sở dữ liệu cho bệnh viện liên quan đến benhVienId
                var benhVien = _context.BenhVien.FirstOrDefault(bv => bv.Id == parsedBenhVienId);
                if (benhVien != null)
                {
                    benhVien.Image_Path =  ImageFile.FileName;
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("caiDatTaiKhoan");
        }


        [HttpPost]
        public IActionResult chinhSuaThongTin(ThongTinBenhVienViewModel model)
        {
            var benhVienId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID của bệnh viện từ claims

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

            // Tìm tài khoản bệnh viện hiện tại dựa trên benhVienId
            var taiKhoan = _context.TaiKhoans.Include(t => t.BenhVien) // Include BenhVien để lấy thông tin bệnh viện
                                              .FirstOrDefault(t => t.BenhVien.Id == parsedBenhVienId);

            if (taiKhoan == null)
            {
                return NotFound(); // Nếu không tìm thấy tài khoản bệnh viện, trả về lỗi
            }

            // Lấy bệnh viện của tài khoản bệnh viện hiện tại
            var benhVien = _context.BenhVien.FirstOrDefault(bv => bv.Id == parsedBenhVienId);
            if (benhVien == null)
            {
                return NotFound(); // Nếu không tìm thấy bệnh viện, trả về lỗi
            }

          
                // Cập nhật thông tin bệnh viện
                benhVien.Name = model.TenBenhVien;
                benhVien.Phone_Number = model.SoDienThoai;
                benhVien.Address = model.DiaChi;

                // Lưu thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();

                return RedirectToAction("caiDatTaiKhoan"); // Điều hướng về trang chính sau khi cập nhật thành công
            
        }

        [HttpPost]
        public async Task<IActionResult> thayDoiMatKhau(ThongTinBenhVienViewModel model)
        {
          
            var benhVienId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID bệnh viện từ claims

            if (string.IsNullOrEmpty(benhVienId))
            {
                return RedirectToAction("Login", "Users"); // Nếu không có benhVienId, điều hướng đến trang login
            }

            // Chuyển đổi benhVienId thành kiểu int
            if (!int.TryParse(benhVienId, out var parsedBenhVienId))
            {
                return RedirectToAction("Login", "Users");
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
