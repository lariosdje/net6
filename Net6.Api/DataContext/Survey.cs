using System;
using System.Collections.Generic;

namespace Net6.Api.DataContext
{
    public partial class Survey
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public string Answers { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public virtual Activity Activity { get; set; } = null!;
    }
}
