using System.ComponentModel.DataAnnotations;

namespace FurryFeast.Areas.Admin.ViewModels {
    public class StockImageWithoutBitmapViewModel {
        [Required]
        public int ImagesId { get; set; }
        [Required]
        public string ImagesCode { get; set; } = null!;
        [Required]
        public string ImagesDescription { get; set; } = null!;
        [Required]
        public int ImagesFileCrc { get; set; }
    }
}
