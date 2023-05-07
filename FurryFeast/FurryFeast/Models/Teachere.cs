using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class Teachere
    {
        public Teachere()
        {
            PetClasses = new HashSet<PetClass>();
        }

        public int TeacherId { get; set; }
        public string TeacherName { get; set; } = null!;

        public virtual ICollection<PetClass> PetClasses { get; set; }
    }
}
