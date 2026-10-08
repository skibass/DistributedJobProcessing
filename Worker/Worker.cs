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

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        Job? job = await _apiClient.RequestJobAsync(
                            workerId, stoppingToken);

                        if (job != null)
                        {
                            try
                            {
                                await _apiClient.StartJobAsync(
                                    workerId, job.Id, stoppingToken);

                                await _jobExecutor.ExecuteAsync(job);

                                await _apiClient.CompleteJobAsync(
                                    workerId, job.Id, stoppingToken);
                            }
                            catch (OperationCanceledException)
                                when (stoppingToken.IsCancellationRequested)
                            {
                                break;
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(
                                    ex,
                                    "Job {JobId} failed.",
                                    job.Id);

                                try
                                {
                                    await _apiClient.FailedJobAsync(
                                        workerId, job.Id, stoppingToken);
                                }
                                catch (OperationCanceledException)
                                    when (stoppingToken.IsCancellationRequested)
                                {
                                    break;
                                }
                                catch (Exception reportEx)
                                {
                                    _logger.LogError(
                                        reportEx,
                                        "Could not report failure for job {JobId}.",
                                        job.Id);
                                }
                            }
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
                        _logger.LogError(
                            ex,
                            "Error while processing job.");

                        try
                        {
                            await Task.Delay(2000, stoppingToken);
                        }
                        catch (OperationCanceledException)
                            when (stoppingToken.IsCancellationRequested)
                        {
                            break;
                        }
                    }
                }
            }
            finally
            {
                _logger.LogInformation(
                    "Shutting down worker {WorkerId}...",
                    workerId);

                try
                {
                    using CancellationTokenSource timeout =
                        new(TimeSpan.FromSeconds(5));

                    await _apiClient.ShutdownWorkerAsync(
                        workerId,
                        timeout.Token);

                    _logger.LogInformation(
                        "Worker {WorkerId} successfully shut down.",
                        workerId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Failed to shut down worker {WorkerId}.",
                        workerId);
                }
            }
        }
    }
}
