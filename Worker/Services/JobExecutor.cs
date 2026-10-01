using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Handlers;

namespace Worker.Services
{
    public class JobExecutor
    {
        private readonly IEnumerable<IJobHandler> _handlers;

        public JobExecutor(IEnumerable<IJobHandler> handlers)
        {
            _handlers = handlers;
        }

        public async Task ExecuteAsync(Job job)
        {
            IJobHandler? handler = _handlers
                .FirstOrDefault(h => h.JobType == job.Type);

            if (handler == null)
            {
                throw new InvalidOperationException(
                    $"No handler found for job type '{job.Type}'.");
            }

            await handler.ExecuteAsync(job);
        }
    }
}
