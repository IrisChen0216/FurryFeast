using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class StockImage
    {
        public StockImage()
        {
            StockArticles = new HashSet<StockArticle>();
        }

        public int ImagesId { get; set; }
        public string ImagesCode { get; set; } = null!;
        public string ImagesDescription { get; set; } = null!;
        public int ImagesFileCrc { get; set; }
        public byte[] ImagesBitmapFile { get; set; } = null!;

        public virtual ICollection<StockArticle> StockArticles { get; set; }
    }
}
