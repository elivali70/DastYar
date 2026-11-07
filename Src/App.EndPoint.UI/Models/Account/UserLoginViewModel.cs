using System.ComponentModel.DataAnnotations;

namespace App.EndPoint.UI.Models.Account
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "وارد کردن فیلد {0} اجباری است.")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "وارد کردن فیلد {0} اجباری است.")]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}
