using System;
using System.Collections.Generic;

namespace CheckRequestSecurity.Models
{
    public partial class Worker
    {
        public Worker()
        {
            Requests = new HashSet<Request>();
            Visits = new HashSet<Visit>();
        }

        public int Id { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Patronymic { get; set; }
        public int? SubDivisionId { get; set; }
        public int? DepartmentId { get; set; }
        public int? Code { get; set; }

        public virtual Department? Department { get; set; }
        public virtual SubDivision? SubDivision { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
