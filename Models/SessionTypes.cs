using System;
using System.Collections.Generic;

namespace DBFirstPlannerAPI.Models
{
    public partial class SessionTypes
    {
        public SessionTypes()
        {
            Sessions = new HashSet<Sessions>();
        }

        public int Id { get; set; }
        public string SessionType { get; set; }

        public virtual ICollection<Sessions> Sessions { get; set; }
    }
}
