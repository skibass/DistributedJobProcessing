using Domain.Enums;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Job
    {
        public Guid Id { get; set; }

        public string Type { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;

        public JobStatus Status { get; set; }
        public JobPriority Priority { get; set; }

        public int RetryCount { get; set; }
        public int MaxRetries { get; set; }

        public Guid? WorkerId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public void Start()
        {
            if (Status != JobStatus.Assigned)
                throw new InvalidJobStateException(
                    "Only assigned jobs can be started.");

            Status = JobStatus.Running;
            StartedAt = DateTime.UtcNow;
        }

        public void Complete()
        {
            if (Status != JobStatus.Running)
                throw new InvalidJobStateException(
                    "Only running jobs can be completed.");

            Status = JobStatus.Completed;
            CompletedAt = DateTime.UtcNow;
        }
    }
}
