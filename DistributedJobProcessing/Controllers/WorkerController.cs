using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DistributedJobProcessing.Controllers
{
    public class WorkerController : Controller
    {
        private readonly IWorkerService _workerService;

        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        [HttpPost("/add")]
        public Worker AddWorker()
        {            
            return _workerService.AddWorker();
        }

        [HttpGet("/get/{id}")]
        public Worker GetWorkerById(Guid id)
        {
            return _workerService.GetWorkerById(id);
        }

        [HttpGet("/get")]
        public List<Worker> GetWorkers(int amount)
        {
            return _workerService.GetWorkers(amount);
        }

    }
}
