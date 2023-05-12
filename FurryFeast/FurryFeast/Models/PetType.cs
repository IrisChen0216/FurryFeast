using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class PetType
    {
        public PetType()
        {
            GameQues = new HashSet<GameQue>();
            PetClasses = new HashSet<PetClass>();
            ProductTypes = new HashSet<ProductType>();
            Recipes = new HashSet<Recipe>();
        }

        public int PetTypesId { get; set; }
        public string PetTypes { get; set; } = null!;

        public virtual ICollection<GameQue> GameQues { get; set; }
        public virtual ICollection<PetClass> PetClasses { get; set; }
        public virtual ICollection<ProductType> ProductTypes { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
