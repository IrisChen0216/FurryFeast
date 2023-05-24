using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class ContactUs
    {
        public int GuestId { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
        public int? GuestPhone { get; set; }
        public string GuestSubject { get; set; }
        public string GuestContext { get; set; }
    }
}