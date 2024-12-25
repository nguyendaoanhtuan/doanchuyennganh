using HTQuanLyHoSoSucKhoe.Models;
using HTQuanLyHoSoSucKhoe.ViewModel;
using HTQuanLyHoSoSucKhoe.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IActionResult QuanLyHoSoBenhAn() {

            List<QuanLyBenhNhanViewModel> model = new List<QuanLyBenhNhanViewModel>();
            var benhNhans = _context.Users.ToList();
            foreach(var i in benhNhans)
            {
                model.Add(new QuanLyBenhNhanViewModel
                {
                    id = i.Id,
                    hoTen = i.Ho+" "+i.Ten,
                    cccd = i.Cccd,
                    sdt = i.Phone_Number,
                });
            }
            return View(model);
        }
        public IActionResult ChiTietBenhAn(int id)
        {
            var benhNhan = _context.Users.Where(i => i.Id == id).Select(i => new ChiTietBenhAnViewModel
            {
                id = i.Id,
                ho = i.Ho,
                ten = i.Ten,
                cccd = i.Cccd,
                email = i.Email,
                diaChi = i.Address,
                sdt = i.Phone_Number,
                hinhAnh = i.Image_Path ?? "",
                hoSoBenhAns = _context.HoSoBenhAns.Where(i => i.UserId == id).ToList()
            }).FirstOrDefault();
            if (benhNhan == null)
            {
                return NotFound("Không tìm thấy thông tin bác sĩ.");
            }
            return View(benhNhan);
        }
        public IActionResult DanhSachHenKham()
        {
            var chuyenKhoaId = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int parsedChuyenKhoaId) ;
            var henKhams = _context.Appointments.Where(i => i.ChuyenKhoaId == parsedChuyenKhoaId).Select(i => new DanhSachHenKhamViewModel
            {
                id = i.Id,
                soThuTu = i.soThuTu,
                thoiGianTao = i.Appointment_Date,
                trangThai = (i.taoHoSo == true) ? "Đã tạo hồ sơ" : "Chưa tạo hồ sơ",
                cccd = i.User.Cccd,
                tenBenhNhan = i.User.Ho + " "+i.User.Ten,
            }).ToList();
            var bacSiList = _context.BacSis.Where(i => i.ChuyenKhoaId == parsedChuyenKhoaId).ToList();
            ViewBag.BacSiList = bacSiList;
            ViewBag.ChuyenKhoaOptions = _context.ChuyenKhoas
            .Where(ck => ck.RoleId == 4)
            .Select(ck => new SelectListItem
            {
                Value = ck.Id.ToString(),
                Text = ck.Name
            }).ToList();
            return View(henKhams);
        }
        public IActionResult ChiTietHoSo(int id)
        {
            var hoSo = _context.HoSoBenhAns.Where(i => i.Id == id).FirstOrDefault();
            if(hoSo == null)
            {
                hoSo = new HoSoBenhAn {

                };
            }    
            return View();
        }
        [HttpPost]
        public IActionResult TaoHoSo(int BacSiId, DanhSachHenKhamViewModel model)
        {
            var bacSi = _context.BacSis.FirstOrDefault(i => i.Id == BacSiId);
            var hoSo = new HoSoBenhAn { 
                UserId = _context.Users.FirstOrDefault(i=>i.Cccd == model.cccd).Id,
                BenhVienId = bacSi.BenhVienId,
                chuyenKhoaId = bacSi.ChuyenKhoaId,
                bacSiId = bacSi.Id,
            };
            _context.HoSoBenhAns.Add(hoSo);
            _context.SaveChanges();
            // Chuyển hướng đến trang Index sau khi thành công
            return RedirectToAction("DanhSachHenKham");
        }

        [HttpPost]

        public IActionResult TaoPhieuChiDinh(DanhSachHenKhamViewModel model)
        {
            var chuyenKhoaId = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int parsedChuyenKhoaId);
            var chuyenKhoa = _context.ChuyenKhoas
                    .FirstOrDefault(ck => ck.Id == model.SelectedChuyenKhoaId && ck.RoleId == 4);

                if (chuyenKhoa != null)
                {
                    // Save "Phiếu Chỉ Định"
                    var phieuChiDinh = new PhieuChiDinh
                    {
                        appointmentId = model.id,
                        LoaiChiDinh = model .SelectedChuyenKhoaId,
                        chuyenKhoaId = parsedChuyenKhoaId,
                        Created_At = DateTime.Now,
                        bacSiId = model.SelectedBacSiId
                    };

                    _context.PhieuChiDinhs.Add(phieuChiDinh);
                    _context.SaveChanges();

                    TempData["Success"] = "Tạo phiếu chỉ định thành công.";
                }
                else
                {
                    TempData["Error"] = "Chuyên khoa không hợp lệ.";
                    return RedirectToAction("DanhSachHenKham");
                 }


            return RedirectToAction("DanhSachHenKham");
        }

        public IActionResult DanhSachPhieuChiDinh()
        {
            // Lấy chuyenKhoaId từ tài khoản đăng nhập
            var chuyenKhoaId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Lấy danh sách phiếu chỉ định và các thông tin liên quan
            var phieuChiDinhList = _context.PhieuChiDinhs
                .Where(p => p.chuyenKhoaId == chuyenKhoaId)
                .Include(p => p.ChuyenKhoa)
                .Include(p => p.Appointment) // Bao gồm Appointments
                .Include(p => p.ChuyenKhoa.TaiKhoan) // Bao gồm User
                .Select(p => new PhieuChiDinhViewModels
                {
                    Id = p.Id,
                    LoaiChiDinh = _context.ChuyenKhoas
                    .Where(ck => ck.Id == p.LoaiChiDinh) // Tìm tên chuyên khoa dựa trên LoaiChiDinh (ID)
                    .Select(ck => ck.Name)
                    .FirstOrDefault(),
                    Created_At = p.Created_At,
                })
                .ToList();

            return View(phieuChiDinhList);
        }


        public IActionResult DanhSachPhieuKetQua()
        {
            var ChuyenKhoaId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID của Chuyên khoa từ claims

            // Kiểm tra nếu không tìm thấy thông tin Chuyên khoa trong Claims
            if (string.IsNullOrEmpty(ChuyenKhoaId) || !int.TryParse(ChuyenKhoaId, out var parsedChuyenKhoaId))
            {
                return RedirectToAction("Login", "TaiKhoan"); // Điều hướng về trang login nếu không hợp lệ
            }
            var phieuKetQuaList = _context.PhieuKetQuas
                .Where(p => p.PhieuChiDinh.chuyenKhoaId == parsedChuyenKhoaId)
                .Include(p => p.ChuyenKhoa)
                .Include(p => p.Appointment)
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
                    phieuChiDinhId = p.phieuChiDinhId,
                    File1 = Url.Content("~/pdf/PhieuKetQua/" + p.duongDanFile1),
                })
                .ToList();

            return View(phieuKetQuaList);


        }

        [HttpPost]
        public IActionResult XoaPhieuChiDinh(int id)
        {
            // Tìm bản ghi cuộc hẹn cần xóa theo id
            var phieuChiDinh = _context.PhieuChiDinhs.FirstOrDefault(a => a.Id == id);

            if (phieuChiDinh == null)
            {
                // Nếu không tìm thấy bản ghi, trả về thông báo lỗi
                return NotFound("Không tìm thấy cuộc hẹn cần xóa.");
            }

            // Xóa bản ghi khỏi database
            _context.PhieuChiDinhs.Remove(phieuChiDinh);
            _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

            TempData["SuccessMessage"] = "Xóa phiếu kết quả thành công!";

            // Chuyển hướng về trang danh sách sau khi xóa thành công
            return RedirectToAction("DanhSachPhieuChiDinh");
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
                .Where(p => p.chuyenKhoaId == parsedChuyenKhoaId && p.Id == id)
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

            ViewBag.ChuyenKhoaOptions = _context.ChuyenKhoas
            .Where(ck => ck.RoleId == 4)
            .Select(ck => new SelectListItem
            {
                Value = ck.Id.ToString(),
                Text = ck.Name
            }).ToList();
            // Kiểm tra nếu không tìm thấy phiếu kết quả
            if (phieuChiDinh == null)
            {
                return NotFound("Phiếu kết quả không tồn tại hoặc bạn không có quyền truy cập.");
            }

            return View(phieuChiDinh);
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
                .Where(p => p.PhieuChiDinh.chuyenKhoaId == parsedChuyenKhoaId && p.Id == id)
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
        public IActionResult ChinhSuaPhieuChiDinh(int phieuChiDinhId, PhieuChiDinhViewModels model)
        {
            var ChuyenKhoaId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID của Chuyên khoa từ claims

            // Kiểm tra nếu không tìm thấy thông tin Chuyên khoa trong Claims
            if (string.IsNullOrEmpty(ChuyenKhoaId) || !int.TryParse(ChuyenKhoaId, out var parsedChuyenKhoaId))
            {
                return RedirectToAction("Login", "TaiKhoan"); // Điều hướng về trang login nếu không hợp lệ
            }

            var chuyenKhoa = _context.ChuyenKhoas
                       .FirstOrDefault(ck => ck.Id == model.SelectedChuyenKhoaId && ck.RoleId == 4);


            // Lấy phiếu chỉ định từ database
            var phieuChiDinh = _context.PhieuChiDinhs.FirstOrDefault(p=>p.Id == phieuChiDinhId);
            if (phieuChiDinh == null)
            {
                return NotFound();
            }

            // Cập nhật thông tin phiếu chỉ định
            phieuChiDinh.LoaiChiDinh = model.SelectedChuyenKhoaId;
           

            // Lưu thay đổi vào database
            _context.Update(phieuChiDinh);
            _context.SaveChanges();

            // Quay về trang danh sách hoặc thông báo thành công
            return RedirectToAction("ChiTietPhieuChiDinh", new { id = phieuChiDinhId });
        }
    }


}
