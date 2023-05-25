using System.ComponentModel.DataAnnotations;

namespace FurryFeast.Areas.Admin.ViewModels {
	public class StockWarehouseViewModel {
		[Required]
		public int WarehousesId { get; set; }
		[Required]
		public string WarehousesCode { get; set; } = null!;
		[Required]
		public string WarehousesDescription { get; set; } = null!;
		public string? WarehousesStreet { get; set; }
		public string? WarehousesZipCode { get; set; }
		public string? WarehousesCity { get; set; }
		public string? WarehousesCountry { get; set; }
		public string? WarehousesNation { get; set; }
		public int? WarehouseGroupId { get; set; }
	}
}
