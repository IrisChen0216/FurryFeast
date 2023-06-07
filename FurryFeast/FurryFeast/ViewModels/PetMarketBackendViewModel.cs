namespace FurryFeast.ViewModels
{
	public class PetMarketBackendViewModel
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; } = null!;
		public int ProductPrice { get; set; }
		public int ProductAmount { get; set; }
		public int ProductState { get; set; }
		public string ProductDescription { get; set; } = null!;
		public DateTime ProductLaunchedTime { get; set; }
		public DateTime? ProductSoldTime { get; set; }
		public int ProductTypeId { get; set; }


		public List<string>? ProductPicImage { get; set; }
		public List<int>? ProductPicId { get; set; }
		public int ArticlesId { get; set; }

		public string ProductTypeName { get; set; }
	}
}
