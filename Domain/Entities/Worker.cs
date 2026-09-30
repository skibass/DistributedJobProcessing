using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Worker
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public WorkerStatus Status { get; set; }

        public DateTime RegisteredAt { get; set; }
        public DateTime LastHeartbeat { get; set; }

        public Guid? CurrentJobId { get; set; }

        public void Create(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Status = WorkerStatus.Idle;
            RegisteredAt = DateTime.UtcNow;
            LastHeartbeat = DateTime.UtcNow;
        }
    }
}
