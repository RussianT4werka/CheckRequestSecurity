using System;
using System.Collections.Generic;

namespace CheckRequestSecurity.Models
{
    public partial class BlackList
    {
        public int Id { get; set; }
        public int VisitorsId { get; set; }
        public string Reaon { get; set; } = null!;

        public virtual Visitor Visitors { get; set; } = null!;
    }
}
