using FurryFeast.Models;

namespace FurryFeast.ViewModels
{
    public class CartOrderViewModel
    {
        public Member Member { get; set; } = null!;
        public Order Order { get; set; } = null!;
    }
}
