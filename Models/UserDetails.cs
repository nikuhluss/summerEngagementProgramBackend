using System;
using System.Collections.Generic;

namespace DBFirstPlannerAPI.Models
{
    public partial class UserDetails
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }

        public virtual Users User { get; set; }
    }
}
