namespace FurryFeast.Areas.Admin.Controllers
{
    public class AdminResgisterViewModel
    {
        public int AdminId { get; set; }
        public string AdminAccount { get; set; } = null!;
        public string AdminPassword { get; set; } = null!;
        public string AdminName { get; set; } = null!;
    }
}