
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DistributedJobProcessing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        [HttpPost("add")]
        public ActionResult<Worker> AddWorker()
        {
            Worker worker = _workerService.AddWorker();

            return Ok(worker);
        }

        [HttpGet("get/{id:guid}")]
        public ActionResult<Worker> GetWorkerById(Guid id)
        {
            Worker? worker = _workerService.GetWorkerById(id);

            if (worker == null)
                return NotFound();

            return Ok(worker);
        }

        [HttpGet("get")]
        public ActionResult<List<Worker>> GetWorkers()
        {
            return Ok(_workerService.GetWorkers());
        }

        [HttpPost("{workerId:guid}/shutdown")]
        public async Task<IActionResult> ShutdownWorker(Guid workerId)
        {
            await _workerService.ShutdownWorkerAsync(workerId);

            return NoContent();
        }
    }
}
