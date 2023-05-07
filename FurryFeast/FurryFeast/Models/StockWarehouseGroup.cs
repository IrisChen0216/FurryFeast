using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class StockWarehouseGroup
    {
        public StockWarehouseGroup()
        {
            StockWarehouses = new HashSet<StockWarehouse>();
        }

        public int WarehouseGroupsId { get; set; }
        public string WarehouseGroupsCode { get; set; } = null!;
        public string WarehouseGroupsDescription { get; set; } = null!;

        public virtual ICollection<StockWarehouse> StockWarehouses { get; set; }
    }
}
