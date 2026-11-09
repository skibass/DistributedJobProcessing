using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DistributedJobProcessing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonitoringController : Controller
    {
        private readonly IWorkerService _workerService;
        private readonly IMonitoringService _monitoringService;

        public MonitoringController(IWorkerService workerService, IMonitoringService monitoringService)
        {
            _workerService = workerService;
            _monitoringService = monitoringService;
        }

        [HttpGet("get")]
        public ActionResult<List<Worker>> GetWorkers()
        {
            return Ok(_workerService.GetWorkers());
        }
    }
}
