using Domain.Entities;
using Worker.Services;

namespace Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly JobExecutor _jobExecutor;
        private readonly JobApiClient _apiClient;

        public Worker(ILogger<Worker> logger, JobExecutor jobExecutor, JobApiClient apiClient)
        {
            _logger = logger;
            _jobExecutor = jobExecutor;
            _apiClient = apiClient;
        }

        protected override async Task ExecuteAsync(
     CancellationToken stoppingToken)
        {
            Guid workerId = await _apiClient.RegisterWorkerAsync(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    Job? job = await _apiClient.RequestJobAsync(
                        workerId, stoppingToken);

                    if (job != null)
                    {
                        await _apiClient.StartJobAsync(
                            workerId, job.Id, stoppingToken);

                        _logger.LogInformation(
                            "Job {JobId} started.", job.Id);

                        await _jobExecutor.ExecuteAsync(job);

                        await _apiClient.CompleteJobAsync(
                            workerId, job.Id, stoppingToken);

                        _logger.LogInformation(
                            "Job {JobId} completed successfully.", job.Id);
                    }
                    else
                    {
                        await Task.Delay(2000, stoppingToken);
                    }
                }
                catch (OperationCanceledException)
                    when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while processing job.");

                    await Task.Delay(2000, stoppingToken);
                }
            }
        }
    }
}
