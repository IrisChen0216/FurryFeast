namespace FurryFeast.ViewModels
{
	public class MsgBoardViewModel
	{
		public int MsgId { get; set; }
		public int MsgRecipesId { get; set; }
		public string UserId { get; set; } = null!;
		public string MsgContent { get; set; } = null!;
		public DateTime MsgDateTime { get; set; }
		public string MsgActive { get; set; } = null!;
	}
}
