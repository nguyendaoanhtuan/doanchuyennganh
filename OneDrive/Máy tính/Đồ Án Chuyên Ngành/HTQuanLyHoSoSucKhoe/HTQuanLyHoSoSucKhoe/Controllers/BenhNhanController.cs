using HTQuanLyHoSoSucKhoe.Models;
using Microsoft.AspNetCore.Mvc;
using HTQuanLyHoSoSucKhoe.ViewModels;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace HTQuanLyHoSoSucKhoe.Controllers
{
	public class BenhNhanController : Controller
	{
		private readonly ApplicationDbContext _context;
		public BenhNhanController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			// Lấy thông tin người dùng đã đăng nhập
			var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(userId))
			{
				return NotFound("User not found.");
			}
			int userIdInt = int.Parse(userId);
			var user = _context.Users.FirstOrDefault(u => u.Id == userIdInt);
			var viewModel = new ThongTinBenhNhanViewModel()
			{
				hoTen = user.hoVaTen,
			};
			return View(viewModel);
		}
	}
}
