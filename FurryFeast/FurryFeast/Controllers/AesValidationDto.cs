using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FurryFeast.Controllers
{
	internal class AesValidationDto
	{
        public AesValidationDto(string MemberAccount, DateTime ExpiredDay)
        {
            this.MemberAccount = MemberAccount;
            this.ExpiredDate = ExpiredDate;
        }
        public string MemberAccount { get; set; }
		public DateTime ExpiredDate { get; set; }
	}
}