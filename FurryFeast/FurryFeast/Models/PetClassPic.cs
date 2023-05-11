using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class PetClassPic
    {
        public int PetClassPicId { get; set; }
        public byte[]? PetClassPicImage { get; set; }
        public int PetClassId { get; set; }

        public virtual PetClass PetClass { get; set; } = null!;
    }
}
