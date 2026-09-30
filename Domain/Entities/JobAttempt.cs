using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class JobAttempt
    {
        public Guid Id { get; set; }

        public Guid JobId { get; set; }
        public Guid WorkerId { get; set; }

        public int AttemptNumber { get; set; }

        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }

        public bool Successful { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
