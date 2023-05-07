using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class StockGroup
    {
        public StockGroup()
        {
            StockArticles = new HashSet<StockArticle>();
        }

        public int GroupsId { get; set; }
        public string GroupsCode { get; set; } = null!;
        public string GgroupsDescription { get; set; } = null!;
        public string? GroupsNotes { get; set; }

        public virtual ICollection<StockArticle> StockArticles { get; set; }
    }
}
