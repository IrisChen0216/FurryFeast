namespace FurryFeast.Areas.Admin.ViewModels {
	public class StockArticleViewModel {
		public int ArticlesId { get; set; }
		public string AarticlesCode { get; set; } = null!;
		public decimal ArticlesIsValid { get; set; }
		public string ArticlesDescription { get; set; } = null!;
		public string? ArticlesNotes { get; set; }
		public decimal? ArticlesPrice { get; set; }
		public decimal? ArticlesQuantity { get; set; }
		public int? GroupId { get; set; }
		public int MeasureUnitId { get; set; }
		public int WarehousesId { get; set; }
		public int SuppliersId { get; set; }
		public int? ImagesId { get; set; }
	}
}
