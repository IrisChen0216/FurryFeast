namespace FurryFeast.ViewModels
{
    public class MemberViewModel
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; } = null!;
        public DateTime MemberBirthday { get; set; }
        public string MemberAdress { get; set; } = null!;
        public string MemberEmail { get; set; } = null!;
        public string MemberPhone { get; set; } = null!;
        public int MemberGender { get; set; }
        public string MemberAccount { get; set; } = null!;
        public string MemberPassord { get; set; } = null!;
    }
}