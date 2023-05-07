using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class Shelter
    {
        public string ShelterId { get; set; } = null!;
        public string ShelterAdress { get; set; } = null!;
        public int ShelterPhone { get; set; }
        public int ShelterDays { get; set; }
        public string AdoptYn { get; set; } = null!;
    }
}
