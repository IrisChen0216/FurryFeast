namespace FurryFeast.ViewModels
{
	public class AddProductImageViewModel
	{
		public int ProductId { get; set; }
		public int ProductPicId { get; set; }
		public List<IFormFile>? ProductPicImage { get; set; }
	}
}
