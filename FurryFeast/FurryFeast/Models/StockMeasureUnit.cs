using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class StockMeasureUnit
    {
        public StockMeasureUnit()
        {
            StockArticles = new HashSet<StockArticle>();
        }

        public int MeasureUnitsId { get; set; }
        public string MeasureUnitsCode { get; set; } = null!;
        public string MeasureUnitsDescription { get; set; } = null!;

        public virtual ICollection<StockArticle> StockArticles { get; set; }
    }
}
