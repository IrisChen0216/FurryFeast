namespace FurryFeast.Areas.Admin.ViewModels
{
    public class EditMemberDto
    {
		public int MemberId { get; set; }
		public string MemberName { get; set; } = null!;
        public DateTime MemberBirthday { get; set; }
        public string? MemberAdress { get; set; }
        public string? MemberEmail { get; set; }
        public string MemberPhone { get; set; } = null!;
        public int MemberGender { get; set; }

    }
}