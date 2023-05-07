using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class StockWarehouse
    {
        public StockWarehouse()
        {
            StockArticles = new HashSet<StockArticle>();
        }

        public int WarehousesId { get; set; }
        public string WarehousesCode { get; set; } = null!;
        public string WarehousesDescription { get; set; } = null!;
        public string? WarehousesStreet { get; set; }
        public string? WarehousesZipCode { get; set; }
        public string? WarehousesCity { get; set; }
        public string? WarehousesCountry { get; set; }
        public string? WarehousesNation { get; set; }
        public int? WarehouseGroupId { get; set; }

        public virtual StockWarehouseGroup? WarehouseGroup { get; set; }
        public virtual ICollection<StockArticle> StockArticles { get; set; }
    }
}
