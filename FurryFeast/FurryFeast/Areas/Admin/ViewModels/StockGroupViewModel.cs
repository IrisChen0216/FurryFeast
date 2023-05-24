namespace FurryFeast.Areas.Admin.ViewModels {
	public class StockGroupViewModel {
		public int GroupsId { get; set; }
		public string GroupsCode { get; set; } = null!;
		public string GgroupsDescription { get; set; } = null!;
		public string? GroupsNotes { get; set; }
	}
}
