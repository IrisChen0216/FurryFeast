namespace FurryFeast.ViewModels
{
    public class PayReservationViewModel
    {
        public int MemberId { get; set; }
        //public int PetClassId { get; set; }
        public string PetClassName { get; set; }
        public int PetClassPrice { get; set; }
        public DateTime PetClassDate { get; set; }
        public int TeacherId { get; set; }
        public int PetClassTypeId { get; set; }
        public string MemberName { get; set; }
        public string MemberEmail { get; set; }
        public string MemberPhone { get; set; }
        public int ClassReservetionId { get; set; }
        public DateTime ClassReservetionDate { get; set; }
        public int ClassReservetionState { get; set; }
        
    }
}
