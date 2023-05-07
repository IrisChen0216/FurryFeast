using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string AdminAccount { get; set; } = null!;
        public string AdminPassword { get; set; } = null!;
        public string AdminName { get; set; } = null!;
    }
}
