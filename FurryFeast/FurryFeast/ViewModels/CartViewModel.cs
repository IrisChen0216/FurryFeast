using FurryFeast.Models;
using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.ViewModels
{
    public class CartViewModel
    {
        public int ProductID { get; set; }
        public int Amount { get; set; }
        public int Subtotal { get; set; }
        public int Price { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
    }
}
