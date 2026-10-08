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
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepo;
        private readonly IWorkerRepository _workerRepo;

        public JobService(IJobRepository jobRepo, IWorkerRepository workerRepo)
        {
            _jobRepo = jobRepo;
            _workerRepo = workerRepo;
        }

        public Job CreateJob(string type, string payload, JobPriority priority, int maxRetries)
        {
            Job job = new Job();
            job.Create(type, payload, priority, maxRetries);


            _jobRepo.CreateJob(job);

            return job;
        }

        public Job GetJobById(Guid id)
        {
            return _jobRepo.GetJobById(id);
        }
        public async Task StartJobAsync(Guid workerId, Guid jobId)
        {
            Job? job = _jobRepo.GetJobById(jobId);
            Worker? worker = _workerRepo.GetWorkerById(workerId);

            if (job == null || worker == null)
                throw new InvalidOperationException("Job or worker not found.");

            if (job.WorkerId != workerId ||
                worker.CurrentJobId != jobId)
            {
                throw new InvalidOperationException(
                    "Job is not assigned to this worker.");
            }

            job.Start();

            await _jobRepo.SaveChangesAsync();
        }


        public async Task CompleteJobAsync(Guid workerId, Guid jobId)
        {
            Job? job = _jobRepo.GetJobById(jobId);
            Worker? worker = _workerRepo.GetWorkerById(workerId);

            if (job == null || worker == null)
                throw new InvalidOperationException("Job or worker not found.");

            if (job.WorkerId != workerId || worker.CurrentJobId != jobId)
                throw new InvalidOperationException("Job is not assigned to this worker.");

            job.Complete();
            worker.CompleteJob(jobId);

            await _jobRepo.SaveChangesAsync();
        }

        public Job GetNextQueuedJob()
        {
            return _jobRepo.GetNextQueuedJob();         
        }
    }
}
