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

        public string PetClassId { get; set; } = null!;
        public string PetClassName { get; set; } = null!;
        public int PetClassPrice { get; set; }
        public string PetClassInformation { get; set; } = null!;
        public int TeacherId { get; set; }
        public DateTime PetClassDate { get; set; }

        public virtual Teachere Teacher { get; set; } = null!;
        public virtual ICollection<ClassReservetion> ClassReservetions { get; set; }
        public virtual ICollection<PetClassPic> PetClassPics { get; set; }
    }
}
