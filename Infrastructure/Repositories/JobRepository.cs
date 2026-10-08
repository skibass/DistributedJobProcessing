
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;

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

        public Job? GetNextQueuedJob()
        {
            return _context.Jobs
                .Where(j => j.Status == JobStatus.Queued)
                .OrderBy(j => j.CreatedAt)
                .FirstOrDefault();
        }

        public Job? GetJobById(Guid id)
        {
            return _context.Jobs
                .FirstOrDefault(j => j.Id == id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
