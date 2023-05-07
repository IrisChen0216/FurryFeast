using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public DateTime OrderCreateDate { get; set; }
        public DateTime OrderShipDate { get; set; }
        public string OrderRecipientName { get; set; } = null!;
        public string OrderRecipientAdress { get; set; } = null!;
        public string OrderRecipientPhone { get; set; } = null!;
        public int OrderTotalPrice { get; set; }
        public int? OrderStatus { get; set; }
        public int MemberId { get; set; }

        public virtual Member Member { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
