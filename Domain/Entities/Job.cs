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

        public void Create(string type, string payload, JobPriority priority = JobPriority.Normal, int maxRetries = 3)
        {
            Id = Guid.NewGuid();
            Type = type;
            Payload = payload;
            Status = JobStatus.Queued;
            Priority = priority;
            RetryCount = 0;
            MaxRetries = maxRetries;
            CreatedAt = DateTime.UtcNow;
        }

        public void AssignToWorker(Guid workerId)
        {
            if (Status != JobStatus.Queued)
                throw new InvalidJobStateException(
                    "Only queued jobs can be assigned to a worker.");
            WorkerId = workerId;
            Status = JobStatus.Assigned;
        }

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
            if (Status != JobStatus.Assigned &&
                Status != JobStatus.Running)
            {
                throw new InvalidJobStateException(
                    "Only assigned or running jobs can be completed.");
            }

            Status = JobStatus.Completed;
            CompletedAt = DateTime.UtcNow;
        }
    }
}
