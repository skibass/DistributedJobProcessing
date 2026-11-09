using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class WorkerResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime LastHeartbeat { get; set; }
        public string? CurrentJobName { get; set; }
        public int? Retries { get; set; }
        public int? MaxRetries { get; set; }
    }
}
