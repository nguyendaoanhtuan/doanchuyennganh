using HTQuanLyHoSoSucKhoe.Controllers;

namespace HTQuanLyHoSoSucKhoe.Services
{
    public class TrangThaiHostedService : IHostedService
    {
        private readonly TrangThaiUpdaterService _updaterService;

        public TrangThaiHostedService(TrangThaiUpdaterService updaterService)
        {
            _updaterService = updaterService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Lập lịch chạy mỗi ngày
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await _updaterService.UpdateTrangThaiPhieuAsync();
                    await Task.Delay(TimeSpan.FromDays(1), cancellationToken);
                }
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
