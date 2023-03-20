using System;
using System.Collections.Generic;

namespace CheckRequestSecurity.Models
{
    public partial class User
    {
        public User()
        {
            Requests = new HashSet<Request>();
            Visitors = new HashSet<Visitor>();
        }

        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string? Login { get; set; }
        public string Password { get; set; } = null!;

        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Visitor> Visitors { get; set; }
    }
}
