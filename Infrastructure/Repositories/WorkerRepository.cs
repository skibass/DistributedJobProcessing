using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Persistence;
using Application.Interfaces;

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
        public List<Worker> GetWorkers(int amount)
        {
            return _context.Workers
                .Take(amount)
                .ToList();
        }

        public void ChangeWorkerJob(Guid jobId)
        {
            _context.Workers.FirstOrDefault(w => w.CurrentJobId == jobId);
            _context.SaveChangesAsync();
        }
    }
}
