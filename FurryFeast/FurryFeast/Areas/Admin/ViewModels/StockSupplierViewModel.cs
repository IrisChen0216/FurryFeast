using System.ComponentModel.DataAnnotations;

namespace FurryFeast.Areas.Admin.ViewModels {
	public class StockSupplierViewModel {
		[Required]
		public int SuppliersId { get; set; }
		[Required]
		public string SuppliersCode { get; set; } = null!;
		[Required]
		public string SuppliersDescription { get; set; } = null!;
		public string? SuppliersStreet { get; set; }
		public string? SuppliersZipCode { get; set; }
		public string? SuppliersCity { get; set; }
		public string? SuppliersCountry { get; set; }
		public string? SuppliersNation { get; set; }
		public string? SuppliersPhone { get; set; }
		public string? SuppliersFax { get; set; }
		public string? SuppliersEmail { get; set; }
		public string? SuppliersUrl { get; set; }
		public int? SupplierGroupId { get; set; }
	}
}
