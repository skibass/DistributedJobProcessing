using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DistributedJobProcessing.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost("/create")]
        public Job CreateJob(string type, string payload, JobPriority priority, int maxRetries)
        {
            return _jobService.CreateJob(type, payload, priority = JobPriority.Normal, maxRetries = 3);
        }

        //[HttpGet("/get/{id}")]
        //public Job GetJobById(Guid id)
        //{
        //    return _jobService.GetWorkerById(id);
        //}

        //[HttpGet("/get")]
        //public List<Job> GetJobs(int amount)
        //{
        //    return _jobService.GetWorkers(amount);
        //}
    }
}
