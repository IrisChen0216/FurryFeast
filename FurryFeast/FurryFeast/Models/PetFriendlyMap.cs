using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class PetFriendlyMap
    {
        public int MapPlaceId { get; set; }
        public string MapPlaceName { get; set; } = null!;
        public string MapPlaceAddress { get; set; } = null!;
        public string MapPlacePhone { get; set; } = null!;
        public string MapPlaceLng { get; set; } = null!;
        public string MapPlaceLat { get; set; } = null!;
        public string MapPlaceType { get; set; } = null!;
        public string? MapPlaceNotes { get; set; }
    }
}
