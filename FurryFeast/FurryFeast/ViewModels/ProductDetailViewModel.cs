namespace FurryFeast.ViewModels
{
	public class ProductDetailViewModel
	{
			public int ProductId { get; set; }
			public string ProductName { get; set; } = null!;
			public int ProductPrice { get; set; }
			public int ProductAmount { get; set; }
			public string ProductDescription { get; set; } = null!;
			public int ProductTypeId { get; set; }
			public List<string>? ProductPicImage { get; set; }
			public string ProductTypeName { get; set; }

	}
}
