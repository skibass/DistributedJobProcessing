using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerRepository _repo;

        public WorkerService(IWorkerRepository repo)
        {
            _repo = repo;
        }

        private string RandomWorkerNameGenerator()
        {
            return $"Worker-{Guid.NewGuid().ToString()[..4].ToUpper()}";
        }

        public Worker AddWorker()
        {
            Worker worker = new Worker();
            worker.Create(RandomWorkerNameGenerator());

            _repo.AddWorker(worker);

            return worker;
        }
        public Worker GetWorkerById(Guid id)
        {                      
            return _repo.GetWorkerById(id);
        }

        public List<Worker> GetWorkers(int amount)
        {          
            return _repo.GetWorkers(amount);
        }

    }
}
