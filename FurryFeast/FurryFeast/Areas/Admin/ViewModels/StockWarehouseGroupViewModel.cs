using System.ComponentModel.DataAnnotations;

namespace FurryFeast.Areas.Admin.ViewModels {
	public class StockWarehouseGroupViewModel {
		[Required]
		public int WarehouseGroupsId { get; set; }
		[Required]
		public string WarehouseGroupsCode { get; set; } = null!;
		[Required]
		public string WarehouseGroupsDescription { get; set; } = null!;
	}
}
