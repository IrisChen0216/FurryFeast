using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FurryFeast.Controllers
{
	internal class AesValidationDto
	{
		public string MemberAccount { get; set; }
		public DateTime ExpiredDate { get; set; }
		public AesValidationDto(string MemberAccount, DateTime ExpiredDate)
        {
            this.MemberAccount = MemberAccount;
            this.ExpiredDate = ExpiredDate;
        }
        
	}

}