﻿namespace FurryFeast.ViewModels
{
	public class MsgBoardViewModel
	{
		public int UserId { get; set; }
		public int MsgRecipesId { get; set; }
		public string MsgContent { get; set; } = null!;
		public DateTime MsgDateTime { get; set; }
		public bool MsgActive { get; set; }
	}
}
