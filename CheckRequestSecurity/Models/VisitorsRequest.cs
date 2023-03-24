using System;
using System.Collections.Generic;

namespace CheckRequestSecurity.Models
{
    public partial class VisitorsRequest
    {
        public int Id { get; set; }
        public int VisitorsId { get; set; }
        public int RequestId { get; set; }

        public virtual Request Request { get; set; } = null!;
        public virtual Visitor Visitors { get; set; } = null!;
    }
}
