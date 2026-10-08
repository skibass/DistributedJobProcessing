using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Persistence;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly AppDbContext _context;

        public WorkerRepository(AppDbContext context)
        {
            _context = context;
        }

        public Worker AddWorker(Worker worker)
        {
            _context.Workers.Add(worker);
            _context.SaveChanges();

            return worker;
        }

        public Worker? GetWorkerById(Guid id)
        {
            return _context.Workers
                .FirstOrDefault(worker => worker.Id == id);
        }
        public List<Worker> GetWorkers()
        {
            return _context.Workers
                .ToList();
        }

        public async Task ChangeWorkerJobAsync(Guid workerId, Guid jobId)
        {
            Worker? worker = await _context.Workers
                .FirstOrDefaultAsync(w => w.Id == workerId);

            if (worker == null)
                throw new InvalidOperationException("Worker not found.");

            worker.CurrentJobId = jobId;
            worker.Status = WorkerStatus.Busy;

            await _context.SaveChangesAsync();
        }
    }
}
