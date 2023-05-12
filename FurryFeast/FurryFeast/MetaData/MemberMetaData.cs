using System.ComponentModel.DataAnnotations;

namespace FurryFeast.Models
{
    public class MemberMetaData
    {
        [Display(Name = "姓名")]
        [Required(ErrorMessage ="{0}為必填欄位")]
        public string MemberName { get; set; } = null!;
        
        [Display(Name ="生日")]
        [Required(ErrorMessage = "{0}為必填欄位")]
        public DateTime MemberBirthday { get; set; }

        [Display(Name ="地址")]
        public string MemberAdress { get; set; } = null!;

        [Display(Name ="Email")]
        public string MemberEmail { get; set; } = null!;

        [Display(Name ="手機號碼")]
        [Required(ErrorMessage = "{0}為必填欄位")]
        [StringLength(10,ErrorMessage="{0}格式不符")]
        public string MemberPhone { get; set; } = null!;

        [Display(Name ="性別")]
        [Required(ErrorMessage = "{0}為必填欄位")]
        public int MemberGender { get; set; }

        [Display(Name ="密碼")]
        [Required(ErrorMessage ="{0}為必填欄位")]
        public string MemberPassord { get; set; } = null!;

    }
}