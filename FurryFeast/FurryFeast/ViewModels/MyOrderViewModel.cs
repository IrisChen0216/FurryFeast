namespace FurryFeast.ViewModels
{
    public class MyOrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderCreateDate { get; set; }
        public DateTime OrderShipDate { get; set; }
        public string OrderRecipientName { get; set; } = null!;
        public string OrderRecipientAdress { get; set; } = null!;
        public string OrderRecipientPhone { get; set; } = null!;
        public int OrderTotalPrice { get; set; }
        public int? OrderStatus { get; set; }
        public int MemberId { get; set; }
    }
}