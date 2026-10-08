using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IWorkerService
    {
        public Worker AddWorker();
        public Worker GetWorkerById(Guid id);
        public List<Worker> GetWorkers();
    }
}
