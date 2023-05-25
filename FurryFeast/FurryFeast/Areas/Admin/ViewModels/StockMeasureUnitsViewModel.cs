using System.ComponentModel.DataAnnotations;

namespace FurryFeast.Areas.Admin.ViewModels {
	public class StockMeasureUnitsViewModel {
		[Required]
		public int MeasureUnitsId { get; set; }
		[Required]
		public string MeasureUnitsCode { get; set; } = null!;
		[Required]
		public string MeasureUnitsDescription { get; set; } = null!;
	}
}
