using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly AppDbContext _context;

        public JobRepository(AppDbContext context)
        {
            _context = context;
        }

        public Job CreateJob(Job job)
        {
            _context.Jobs.Add(job);
            _context.SaveChanges();

            return job;
        }

        //public Worker? GetWorkerById(Guid id)
        //{
        //    return _context.Workers
        //        .FirstOrDefault(worker => worker.Id == id);
        //}
        //public List<Worker> GetWorkers(int amount)
        //{
        //    return _context.Workers
        //        .Take(amount)
        //        .ToList();
        //}
    }
}
