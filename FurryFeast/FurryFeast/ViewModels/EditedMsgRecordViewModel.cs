namespace FurryFeast.ViewModels
{
	public class EditedMsgRecordViewModel
	{ 
		public int EditedMsgId { get; set; }
		public int MsgId { get; set; }
		public string EditedText { get; set; } = null!;
		public DateTime EditedTime { get; set; }

	}
}
