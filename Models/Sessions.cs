using System;
using System.Collections.Generic;

namespace DBFirstPlannerAPI.Models
{
    public partial class Sessions
    {
        public Sessions()
        {
            SessionAttendees = new HashSet<SessionAttendees>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? DateHeld { get; set; }
        public string Description { get; set; }
        public int Organizer { get; set; }
        public int? SessionTypeId { get; set; }

        public virtual Users OrganizerNavigation { get; set; }
        public virtual SessionTypes SessionType { get; set; }
        public virtual ICollection<SessionAttendees> SessionAttendees { get; set; }
    }
}
