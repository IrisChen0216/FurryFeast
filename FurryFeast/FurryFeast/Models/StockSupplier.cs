using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class StockSupplier
    {
        public StockSupplier()
        {
            StockArticles = new HashSet<StockArticle>();
        }

        public int SuppliersId { get; set; }
        public string SuppliersCode { get; set; } = null!;
        public string SuppliersDescription { get; set; } = null!;
        public string? SuppliersStreet { get; set; }
        public string? SuppliersZipCode { get; set; }
        public string? SuppliersCity { get; set; }
        public string? SuppliersCountry { get; set; }
        public string? SuppliersNation { get; set; }
        public string? SuppliersPhone { get; set; }
        public string? SuppliersFax { get; set; }
        public string? SuppliersEmail { get; set; }
        public string? SuppliersUrl { get; set; }
        public int? SupplierGroupId { get; set; }

        public virtual StockSuppliersGroup? SupplierGroup { get; set; }
        public virtual ICollection<StockArticle> StockArticles { get; set; }
    }
}
