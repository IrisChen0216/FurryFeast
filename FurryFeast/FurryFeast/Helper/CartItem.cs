using FurryFeast.Models;
using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Helper
{
	public class CartItem 
	{
		public int Id { get; set; }
		public int ProductID { get; set; }
		public int Amount { get; set; }
		public int Subtotal { get; set; }
		public string ProductName { get; set; }
		public string ProductImage { get; set; }
	}
}
