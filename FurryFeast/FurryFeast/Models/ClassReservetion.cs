using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class ClassReservetion
    {
        public int ClassReservetionId { get; set; }
        public int MemberId { get; set; }
        public string PetClassId { get; set; } = null!;
        public DateTime ClassReservetionDate { get; set; }
        public string ClassReservetionState { get; set; } = null!;

        public virtual PetClass PetClass { get; set; } = null!;
    }
}
