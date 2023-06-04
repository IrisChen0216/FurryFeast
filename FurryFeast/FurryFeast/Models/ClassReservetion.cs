using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class ClassReservetion
    {
        public int ClassReservetionId { get; set; }
        public int MemberId { get; set; }
        public int PetClassId { get; set; }
        public DateTime ClassReservetionDate { get; set; }
        public int ClassReservetionState { get; set; }

        public virtual PetClass PetClass { get; set; } = null!;
    }
}
