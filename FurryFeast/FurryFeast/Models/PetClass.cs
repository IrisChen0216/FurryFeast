using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class PetClass
    {
        public PetClass()
        {
            ClassReservetions = new HashSet<ClassReservetion>();
            PetClassPics = new HashSet<PetClassPic>();
        }

        public int PetClassId { get; set; }
        public string PetClassName { get; set; } = null!;
        public int PetClassPrice { get; set; }
        public string PetClassInformation { get; set; } = null!;
        public DateTime PetClassDate { get; set; }
        public int TeacherId { get; set; }
        public int PetTypesId { get; set; }
        public int PetClassTypeId { get; set; }

        public virtual PetClassType PetClassType { get; set; } = null!;
        public virtual PetType PetTypes { get; set; } = null!;
        public virtual Teachere Teacher { get; set; } = null!;
        public virtual ICollection<ClassReservetion> ClassReservetions { get; set; }
        public virtual ICollection<PetClassPic> PetClassPics { get; set; }
    }
}
