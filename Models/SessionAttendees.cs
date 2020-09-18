using System;
using System.Collections.Generic;

namespace DBFirstPlannerAPI.Models
{
    public partial class SessionAttendees
    {
        public int UserId { get; set; }
        public int SessionId { get; set; }

        public virtual Sessions Session { get; set; }
        public virtual Users User { get; set; }
    }
}
