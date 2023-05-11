using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class PetClassType
    {
        public PetClassType()
        {
            PetClasses = new HashSet<PetClass>();
        }

        public int PetClassTypeId { get; set; }
        public string PetClassTypeName { get; set; } = null!;

        public virtual ICollection<PetClass> PetClasses { get; set; }
    }
}
