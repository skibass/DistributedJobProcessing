
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DistributedJobProcessing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost("create")]
        public ActionResult<Job> CreateJob(
            [FromQuery] string type,
            [FromQuery] string payload,
            [FromQuery] JobPriority priority = JobPriority.Normal,
            [FromQuery] int maxRetries = 3)
        {
            Job job = _jobService.CreateJob(
                type,
                payload,
                priority,
                maxRetries);

            return Ok(job);
        }

        [HttpPost("{jobId:guid}/start")]
        public async Task<IActionResult> StartJob(
    Guid jobId,
    [FromQuery] Guid workerId)
        {
            await _jobService.StartJobAsync(workerId, jobId);

            return NoContent();
        }

        [HttpPost("{jobId:guid}/complete")]
        public async Task<IActionResult> CompleteJob(
    Guid jobId,
    [FromQuery] Guid workerId)
        {
            await _jobService.CompleteJobAsync(workerId, jobId);

            return NoContent();
        }
    }

}
