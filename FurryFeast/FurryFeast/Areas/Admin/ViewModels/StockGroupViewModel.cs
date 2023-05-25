using System.ComponentModel.DataAnnotations;

namespace FurryFeast.Areas.Admin.ViewModels {
	public class StockGroupViewModel {
		[Required]
		public int GroupsId { get; set; }
		[Required]
		public string GroupsCode { get; set; } = null!;
		[Required]
		public string GgroupsDescription { get; set; } = null!;
		public string? GroupsNotes { get; set; }
	}
}
