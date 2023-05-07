using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ProductPics = new HashSet<ProductPic>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int ProductPrice { get; set; }
        public int ProductAmount { get; set; }
        public string ProductDescription { get; set; } = null!;
        public int ProductState { get; set; }
        public DateTime ProductLaunchedTime { get; set; }
        public DateTime ProductSoldTime { get; set; }
        public int ProductTypeId { get; set; }
        public int ArticlesId { get; set; }

        public virtual StockArticle Articles { get; set; } = null!;
        public virtual ProductType ProductType { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductPic> ProductPics { get; set; }
    }
}
