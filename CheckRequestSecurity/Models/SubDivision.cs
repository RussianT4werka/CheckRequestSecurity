using System;
using System.Collections.Generic;

namespace CheckRequestSecurity.Models
{
    public partial class SubDivision
    {
        public SubDivision()
        {
            Workers = new HashSet<Worker>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Worker> Workers { get; set; }
    }
}
