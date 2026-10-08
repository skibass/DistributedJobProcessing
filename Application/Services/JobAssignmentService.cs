using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class JobAssignmentService : IJobAssignmentService
    {
        private readonly IWorkerRepository _workerRepo;
        private readonly IJobRepository _jobRepo;

        public JobAssignmentService(IWorkerRepository workerRepo, IJobRepository jobRepository) 
        {
            _workerRepo = workerRepo;
            _jobRepo = jobRepository;
        }

        private Worker FindAvailableWorker()
        {
            return _workerRepo.GetWorkers().FirstOrDefault(w => w.Status != WorkerStatus.Idle);
        }

        public async Task<Job?> AssignJobToWorkerAsync(Guid workerId)
        {
            Worker? worker = _workerRepo.GetWorkerById(workerId);

            if (worker == null)
                throw new InvalidOperationException("Worker not found.");

            if (worker.Status != WorkerStatus.Idle)
                throw new InvalidOperationException("Worker is not available.");

            Job? job = _jobRepo.GetNextQueuedJob();

            if (job == null)
                return null;

            job.AssignToWorker(worker.Id);
            worker.AssignJob(job.Id);

            await _jobRepo.SaveChangesAsync();

            return job;
        }
    }
}
