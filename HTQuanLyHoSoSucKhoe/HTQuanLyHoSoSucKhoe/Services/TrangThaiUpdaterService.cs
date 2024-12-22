using HTQuanLyHoSoSucKhoe.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HTQuanLyHoSoSucKhoe.Controllers
{
    public class TrangThaiUpdaterService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<TrangThaiUpdaterService> _logger;

        public TrangThaiUpdaterService(IServiceProvider serviceProvider, ILogger<TrangThaiUpdaterService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task UpdateTrangThaiPhieuAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var today = DateTime.Now.Date;
                var appointments = dbContext.Appointments.ToList();

                foreach (var appointment in appointments)
                {
                    if (appointment.Appointment_Date.Date == today)
                    {
                        appointment.trangThaiPhieu = "Đang thăm khám";
                    }
                    else if (appointment.Appointment_Date.Date < today)
                    {
                        appointment.trangThaiPhieu = "Đã thăm khám";
                    }
                }

                await dbContext.SaveChangesAsync();
                _logger.LogInformation("Cập nhật trạng thái phiếu thăm khám thành công.");
            }
        }
    }
}
