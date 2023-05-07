using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class Animal
    {
        public string AnimalId { get; set; } = null!;
        public string AnimalType { get; set; } = null!;
        public string? MicrochipId { get; set; }
        public string AnimalBreed { get; set; } = null!;
        public string AnimalPattern { get; set; } = null!;
        public string AnimalSex { get; set; } = null!;
        public string FeedingMethod { get; set; } = null!;
        public string AnimalFeature { get; set; } = null!;
        public string AnimalName { get; set; } = null!;
    }
}
