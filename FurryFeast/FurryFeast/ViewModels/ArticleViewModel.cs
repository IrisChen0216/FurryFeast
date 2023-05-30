namespace FurryFeast.ViewModels
{
	public class ArticleViewModel
	{
		public int AdminId { get; set; }
		public string ArticleTitle { get; set; } = null!;
		public string ArticleText { get; set; } = null!;
		public DateTime ArticleDate { get; set; }
		public int ArticleId { get; set; }

	}
}
