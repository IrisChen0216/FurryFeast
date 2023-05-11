using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class LostAnimal
    {
        public int LostAnimalId { get; set; }
        public DateTime LostDay { get; set; }
        public int? MicrochipId { get; set; }
        public string LostPlace { get; set; } = null!;
        public string AnimalPattern { get; set; } = null!;
        public string AnimalSex { get; set; } = null!;
        public string? AnimalFeature { get; set; }
        public string? AnimalName { get; set; }
        public string AnimalType { get; set; } = null!;
        public int AnimalAge { get; set; }
        public string AnimalBreed { get; set; } = null!;
    }
}
