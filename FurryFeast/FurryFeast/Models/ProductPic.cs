using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class ProductPic
    {
        public int ProductPicId { get; set; }
        public byte[]? ProductPicImage { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
