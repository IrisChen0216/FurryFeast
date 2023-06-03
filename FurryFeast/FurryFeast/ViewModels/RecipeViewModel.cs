namespace FurryFeast.ViewModels
{
	public class RecipeViewModel
	{
		public int RecipesId { get; set; }
		public int PetTypesId { get; set; }
		public string RecipesName { get; set; } = null!;
		public string? RecipesDescription { get; set; }
		public string RecipesData { get; set; } = null!;
		public string RecipesMethod { get; set; } = null!;
		public string RecipesNotes { get; set; } = null!;
	}
}
