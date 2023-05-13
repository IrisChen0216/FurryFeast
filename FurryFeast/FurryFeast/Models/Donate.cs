using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class Donate
    {
        public int DonateId { get; set; }
        public int DogDonateMoney { get; set; }
        public int DogDonatePeople { get; set; }
        public int DogDonateTotal { get; set; }
        public int CatDonateMoney { get; set; }
        public int CatDonatePeople { get; set; }
        public int CatDonateTotal { get; set; }
    }
}
