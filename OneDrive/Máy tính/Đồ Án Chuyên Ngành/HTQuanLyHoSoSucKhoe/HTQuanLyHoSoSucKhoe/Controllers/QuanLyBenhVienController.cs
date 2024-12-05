using HTQuanLyHoSoSucKhoe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return View();
        }
     
    }
}
