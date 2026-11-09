using Application.DTOs;
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
        public Task<Worker> GetWorkerById(Guid id);
        public Task<List<WorkerResponse>> GetWorkers();
        public Task ShutdownWorkerAsync(Guid workerId);
        public Task UpdateHeartBeatWorker(Guid workerId);
    }
}
