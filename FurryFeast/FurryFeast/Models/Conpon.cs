using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class Conpon
    {
        public Conpon()
        {
            Members = new HashSet<Member>();
        }

        public int ConponId { get; set; }
        public string? ConponContent { get; set; }
        public byte[] ConponName { get; set; } = null!;
        public int ConponDiscount { get; set; }
        public DateTime ConponStartTime { get; set; }
        public DateTime ConponEndTime { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}
