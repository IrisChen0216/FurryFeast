using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class Authority
    {
        public int AuthorityId { get; set; }
        public int AdminId { get; set; }
        public int RoleId { get; set; }

        public virtual Admin Admin { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
