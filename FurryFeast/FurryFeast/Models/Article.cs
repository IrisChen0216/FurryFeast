using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class Article
    {
        public int AdminId { get; set; }
        public string AdminName { get; set; } = null!;
        public string ArticleTitle { get; set; } = null!;
        public string ArticleText { get; set; } = null!;
        public string ArticleType { get; set; } = null!;
        public int ArticleId { get; set; }

        public virtual Admin Admin { get; set; } = null!;
    }
}
