using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class Article
    {
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleText { get; set; }
        public DateTime ArticleDate { get; set; }
        public int ArticleId { get; set; }

        public virtual Admin Admin { get; set; }
    }
}
