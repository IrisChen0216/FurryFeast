using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            MsgBoards = new HashSet<MsgBoard>();
        }

        public int RecipesId { get; set; }
        public int PetTypesId { get; set; }
        public int PhotoId { get; set; }
        public string RecipesName { get; set; } = null!;
        public string? RecipesDescription { get; set; }
        public string RecipesData { get; set; } = null!;
        public string RecipesMethod { get; set; } = null!;
        public string RecipesNotes { get; set; } = null!;

        public virtual PetType PetTypes { get; set; } = null!;
        public virtual RecipesPhoto Photo { get; set; } = null!;
        public virtual ICollection<MsgBoard> MsgBoards { get; set; }
    }
}
