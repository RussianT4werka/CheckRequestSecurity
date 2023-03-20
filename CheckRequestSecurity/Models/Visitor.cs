using System;
using System.Collections.Generic;

namespace CheckRequestSecurity.Models
{
    public partial class Visitor
    {
        public Visitor()
        {
            BlackLists = new HashSet<BlackList>();
            VisitorsRequests = new HashSet<VisitorsRequest>();
            VisitorsVisits = new HashSet<VisitorsVisit>();
        }

        public int Id { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Patronymic { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; } = null!;
        public DateTime DoB { get; set; }
        public string PassportSeries { get; set; } = null!;
        public string PassportNumber { get; set; } = null!;
        public byte[]? Photo { get; set; }
        public string? Organisation { get; set; }
        public string Note { get; set; } = null!;
        public byte[]? ScanPassport { get; set; }
        public int? UsersId { get; set; }

        public virtual User? Users { get; set; }
        public virtual ICollection<BlackList> BlackLists { get; set; }
        public virtual ICollection<VisitorsRequest> VisitorsRequests { get; set; }
        public virtual ICollection<VisitorsVisit> VisitorsVisits { get; set; }
    }
}
