using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class StockSuppliersGroup
    {
        public StockSuppliersGroup()
        {
            StockSuppliers = new HashSet<StockSupplier>();
        }

        public int SuppliersGroupsId { get; set; }
        public string SuppliersGroupsCode { get; set; } = null!;
        public string SuppliersGroupsDescription { get; set; } = null!;

        public virtual ICollection<StockSupplier> StockSuppliers { get; set; }
    }
}
