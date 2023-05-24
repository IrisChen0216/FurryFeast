namespace FurryFeast.Areas.Admin.ViewModels {
	public class StockImageViewModel {
		public int ImagesId { get; set; }
		public string ImagesCode { get; set; } = null!;
		public string ImagesDescription { get; set; } = null!;
		public int ImagesFileCrc { get; set; }
		public byte[] ImagesBitmapFile { get; set; } = null!;
	}
}
