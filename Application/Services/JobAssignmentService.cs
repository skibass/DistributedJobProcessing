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
    public class JobAssignmentService
    {
        private readonly IWorkerRepository _workerRepo;
        private readonly IJobRepository _jobRepo;

        public JobAssignmentService(IWorkerRepository workerRepo, IJobRepository jobRepository) 
        {
            _workerRepo = workerRepo;
            _jobRepo = jobRepository;
        }

        public Worker FindWorkerNotIdle()
        {
            return _workerRepo.GetWorkers(10).FirstOrDefault(w => w.Status == WorkerStatus.Idle);
        }

        public void AssignJobToWorker(Job job)
        {        
            Worker worker = FindWorkerNotIdle();  
  
            job.WorkerId = worker.Id;
            job.Status = JobStatus.Assigned;

            _workerRepo.ChangeWorkerJob(job.Id);
            _jobRepo.ChangeJobWorker(job, worker.Id);
        }
    }
}
