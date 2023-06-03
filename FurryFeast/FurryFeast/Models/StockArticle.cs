using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class StockArticle
    {
        public StockArticle()
        {
            Products = new HashSet<Product>();
        }

        public int ArticlesId { get; set; }
        public string ArticlesCode { get; set; } = null!;
        public decimal ArticlesIsValid { get; set; }
        public string ArticlesDescription { get; set; } = null!;
        public string? ArticlesNotes { get; set; }
        public decimal? ArticlesPrice { get; set; }
        public decimal? ArticlesQuantity { get; set; }
        public int? GroupId { get; set; }
        public int MeasureUnitId { get; set; }
        public int WarehousesId { get; set; }
        public int SuppliersId { get; set; }
        public int? ImagesId { get; set; }

        public virtual StockGroup? Group { get; set; }
        public virtual StockImage? Images { get; set; }
        public virtual StockMeasureUnit MeasureUnit { get; set; } = null!;
        public virtual StockSupplier Suppliers { get; set; } = null!;
        public virtual StockWarehouse Warehouses { get; set; } = null!;
        public virtual ICollection<Product> Products { get; set; }
    }
}
