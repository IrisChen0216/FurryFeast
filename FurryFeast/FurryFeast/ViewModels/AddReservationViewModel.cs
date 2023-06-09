﻿using Newtonsoft.Json.Linq;

namespace FurryFeast.ViewModels
{
    public class AddReservationViewModel
    {      
        public int MemberId { get; set; }
        public int PetClassId { get; set; }
        public string PetClassName { get; set; }
        public int PetClassPrice { get; set; }
        public DateTime PetClassDate { get; set; }
        public int TeacherId { get; set; }
        public int PetClassTypeId { get; set; }
        public string MemberName { get; set; }
        public string MemberEmail { get; set; }
        public string MemberPhone { get; set; }

    }
}
