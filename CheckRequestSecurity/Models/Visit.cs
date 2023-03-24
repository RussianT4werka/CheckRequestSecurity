using System;
using System.Collections.Generic;

namespace CheckRequestSecurity.Models
{
    public partial class Visit
    {
        public Visit()
        {
            VisitorsVisits = new HashSet<VisitorsVisit>();
        }

        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int WorkerId { get; set; }
        public string? GroupNumber { get; set; }
        public DateTime? TimeStartSubDivision { get; set; }
        public DateTime? TimeFinishSubDivision { get; set; }

        public virtual Worker Worker { get; set; } = null!;
        public virtual ICollection<VisitorsVisit> VisitorsVisits { get; set; }
    }
}
