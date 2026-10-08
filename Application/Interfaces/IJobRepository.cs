using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IJobRepository
    {
        public Job CreateJob(Job job);
        public Job GetNextQueuedJob();
        public Job? GetJobById(Guid id);
        Task<int> SaveChangesAsync();

    }
}
