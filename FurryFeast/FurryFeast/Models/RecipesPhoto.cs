using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class RecipesPhoto
    {
        public RecipesPhoto()
        {
            Recipes = new HashSet<Recipe>();
        }

        public int PhotoId { get; set; }
        public byte[] RecipesPhotos { get; set; } = null!;

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
