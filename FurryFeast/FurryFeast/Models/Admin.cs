using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class Admin
    {
        public Admin()
        {
            Articles = new HashSet<Article>();
        }

        public int AdminId { get; set; }
        public string AdminAccount { get; set; } = null!;
        public string AdminPassword { get; set; } = null!;
        public string AdminName { get; set; } = null!;

        public virtual ICollection<Article> Articles { get; set; }
    }
}
