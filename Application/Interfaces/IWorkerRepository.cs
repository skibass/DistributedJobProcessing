using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IWorkerRepository
    {
        public Worker AddWorker(Worker worker);
        public Worker GetWorkerById(Guid id);
        public List<Worker> GetWorkers();
        public Task ChangeWorkerJobAsync(Guid workerId, Guid jobId);

    }
}
