
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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
                .OrderByDescending(j => j.Priority)
                .ThenBy(j => j.CreatedAt)
                .ThenBy(j => j.Id)
                .FirstOrDefault();
        }

        public async Task<Job?> GetJobById(Guid id)
        {
            return await _context.Jobs
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Job>> GetJobs()
        {
            return await _context.Jobs.ToListAsync();
        }
    }
}
