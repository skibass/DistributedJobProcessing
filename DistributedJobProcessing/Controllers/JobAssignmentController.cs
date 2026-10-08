using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DistributedJobProcessing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobAssignmentController : ControllerBase
    {
        private readonly IJobAssignmentService _jobAssignmentService;

        public JobAssignmentController(
            IJobAssignmentService jobAssignmentService)
        {
            _jobAssignmentService = jobAssignmentService;
        }

        [HttpPost("{workerId:guid}/jobs/request")]
        public async Task<ActionResult<Job>> RequestJob(Guid workerId)
        {
            Job? job = await _jobAssignmentService
                .AssignJobToWorkerAsync(workerId);

            if (job == null)
                return NoContent();

            return Ok(job);
        }
    }
}