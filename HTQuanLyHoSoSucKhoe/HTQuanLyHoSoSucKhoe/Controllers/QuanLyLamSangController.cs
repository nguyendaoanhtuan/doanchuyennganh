using HTQuanLyHoSoSucKhoe.Models;
using HTQuanLyHoSoSucKhoe.ViewModel;
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
    }
}
