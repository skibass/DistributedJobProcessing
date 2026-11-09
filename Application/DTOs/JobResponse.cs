using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class JobResponse
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public int RetryCount { get; set; }
        public int MaxRetries { get; set; }
        public DateTime CreatedAt { get; set; }
        public string WorkerName { get; set; } = string.Empty;
    }
}
