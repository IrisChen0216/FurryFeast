using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class Member
    {
        public Member()
        {
            MsgBoards = new HashSet<MsgBoard>();
            Orders = new HashSet<Order>();
        }

        public int MemberId { get; set; }
        public string MemberName { get; set; } = null!;
        public DateTime MemberBirthday { get; set; }
        public string? MemberAdress { get; set; }
        public string? MemberEmail { get; set; }
        public string MemberPhone { get; set; } = null!;
        public int MemberGender { get; set; }
        public string MemberAccount { get; set; } = null!;
        public string MemberPassord { get; set; } = null!;
        public int? ConponId { get; set; }
        public bool Active { get; set; }
        public string? Salt { get; set; }

        public virtual Conpon? Conpon { get; set; }
        public virtual ICollection<MsgBoard> MsgBoards { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
