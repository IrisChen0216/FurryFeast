using System.ComponentModel.DataAnnotations;

namespace FurryFeast.Areas.Admin.ViewModels {
	public class StockArticleViewModel {
		[Required]
		public int ArticlesId { get; set; }
		[Required]
		public string ArticlesCode { get; set; } = null!;
		[Required]
		public decimal ArticlesIsValid { get; set; }
		[Required]
		public string ArticlesDescription { get; set; } = null!;
		public string? ArticlesNotes { get; set; }
		public decimal? ArticlesPrice { get; set; }
		public decimal? ArticlesQuantity { get; set; }
		public int? GroupId { get; set; }
		[Required]
		public int MeasureUnitId { get; set; }
		[Required]
		public int WarehousesId { get; set; }
		[Required]
		public int SuppliersId { get; set; }
		public int? ImagesId { get; set; }
	}
}
