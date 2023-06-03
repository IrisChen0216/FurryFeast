namespace FurryFeast.ViewModels
{
	public class OrdersListViewModel
	{
		public int OrderId { get; set; }
		public string OrderRecipientName { get; set; } = null!;
		public string OrderRecipientAdress { get; set; } = null!;
		public string OrderRecipientPhone { get; set; } = null!;
	}
}
