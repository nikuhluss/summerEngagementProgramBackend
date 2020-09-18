using System;
using System.Collections.Generic;

namespace DBFirstPlannerAPI.Models
{
    public partial class Users
    {
        public Users()
        {
            SessionAttendees = new HashSet<SessionAttendees>();
            Sessions = new HashSet<Sessions>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordSat { get; set; }
        public string PasswordHash { get; set; }

        public virtual UserDetails UserDetails { get; set; }
        public virtual ICollection<SessionAttendees> SessionAttendees { get; set; }
        public virtual ICollection<Sessions> Sessions { get; set; }
    }
}
