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
        private readonly IJobRepository _repo;

        public JobService(IJobRepository repo)
        {
            _repo = repo;
        }

        public Job CreateJob(string type, string payload, JobPriority priority, int maxRetries)
        {
            Job job = new Job();
            job.Create(type, payload, priority, maxRetries);


            _repo.CreateJob(job);

            return job;
        }
        //public Worker GetWorkerById(Guid id)
        //{
        //    return _repo.GetWorkerById(id);
        //}

        //public List<Worker> GetWorkers(int amount)
        //{
        //    return _repo.GetWorkers(amount);
        //}
    }
}
