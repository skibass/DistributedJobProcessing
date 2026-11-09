using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerRepository _workerRepo;
        private readonly IJobRepository _jobRepo;

        public WorkerService(IWorkerRepository workerRepo, IJobRepository jobRepository)
        {
            _workerRepo = workerRepo;
            _jobRepo = jobRepository;
        }

        private string RandomWorkerNameGenerator()
        {
            return $"Worker-{Guid.NewGuid().ToString()[..4].ToUpper()}";
        }

        public Worker AddWorker()
        {
            Worker worker = new Worker();
            worker.Create(RandomWorkerNameGenerator());

            _workerRepo.AddWorker(worker);

            return worker;
        }
        public async Task<Worker> GetWorkerById(Guid id)
        {                      
            return await _workerRepo.GetWorkerById(id);
        }

        public async Task<List<WorkerResponse>> GetWorkers()
        {
            var workers = await _workerRepo.GetWorkers();
            var result = new List<WorkerResponse>();

            foreach (var worker in workers)
            {
                string jobName = "No job currently";
                int? retries = null;
                int? maxRetries = null;

                if (worker.CurrentJobId.HasValue)
                {
                    var job = await _jobRepo.GetJobById(
                        worker.CurrentJobId.Value);

                    if (job != null)
                    {
                        jobName = job.Type.ToString();
                        retries = job.RetryCount;
                        maxRetries = job.MaxRetries;
                    }
                }

                result.Add(new WorkerResponse
                {
                    Id = worker.Id,
                    Name = worker.Name,
                    Status = worker.Status.ToString(),
                    LastHeartbeat = worker.LastHeartbeat,
                    CurrentJobName = jobName,
                    Retries = retries,
                    MaxRetries = maxRetries
                });
            }

            return result;
        }

        public async Task ShutdownWorkerAsync(Guid workerId)
        {
            Worker? worker = await _workerRepo.GetWorkerById(workerId);

            if (worker == null)
                throw new InvalidOperationException("Worker not found.");

            worker.SetOffline();

            await _workerRepo.SaveChangesAsync();
        }

        public async Task UpdateHeartBeatWorker(Guid workerId)
        {
            Worker? worker = await _workerRepo.GetWorkerById(workerId);

            if (worker == null)
                throw new InvalidOperationException("Worker not found.");

            worker.UpdateHeartBeat();

            await _workerRepo.SaveChangesAsync();
        }

    }
}
