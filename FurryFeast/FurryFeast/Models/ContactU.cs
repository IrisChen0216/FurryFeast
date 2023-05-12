using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class ContactU
    {
        public int GuestId { get; set; }
        public string GuestName { get; set; } = null!;
        public string GuestEmail { get; set; } = null!;
        public int? GuestPhone { get; set; }
        public string GuestSubject { get; set; } = null!;
        public string GuestContext { get; set; } = null!;
    }
}
