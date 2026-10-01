using Worker.Services;

namespace Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly JobExecutor _jobExecutor;

        public Worker(ILogger<Worker> logger, JobExecutor jobExecutor)
        {
            _logger = logger;
            _jobExecutor = jobExecutor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await _jobExecutor.ExecuteAsync(new Domain.Entities.Job
                {
                    Type = "CheckImageExists",
                    Payload = "C://Users//thijn//Pictures//Screenshots//Screenshot 2025-09-02 112600.png"
                });
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
