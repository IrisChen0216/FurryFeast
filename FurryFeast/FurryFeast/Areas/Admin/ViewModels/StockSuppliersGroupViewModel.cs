using System.ComponentModel.DataAnnotations;

namespace FurryFeast.Areas.Admin.ViewModels {
	public class StockSuppliersGroupViewModel {
		[Required]
		public int SuppliersGroupsId { get; set; }
		[Required]
		public string SuppliersGroupsCode { get; set; } = null!;
		[Required]
		public string SuppliersGroupsDescription { get; set; } = null!;
	}
}
