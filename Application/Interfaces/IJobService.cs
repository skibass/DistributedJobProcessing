using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IJobService
    {
        public Job CreateJob(string type, string payload, JobPriority priority = JobPriority.Normal, int maxRetries = 3);
    }
}
