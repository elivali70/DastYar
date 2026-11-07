using System.ComponentModel.DataAnnotations;

namespace App.EndPoint.UI.Models.Account
{
    public class UserViewModel
    {
        [Display(Name ="شناسه کاربر")]
        public int Id { get; set; }

        [Required]
        [Display(Name ="نام")]
        public string Name {  get; set; }


        [Required(ErrorMessage = "وارد کردن {0} اجباری است.")]
        [Display(Name = "نام و نام خانوادگی ")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "وارد کردن {0} اجباری است.")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name ="شماره تماس")]
        public string PhoneNumber {  get; set; }

        [Display(Name = "نقش‌های کاربر")]
        public string Roles { get; set; }

        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "{0}کلمه عبور حداقل {1} کارکتر و حداکثر {2} کارکتر باشد", MinimumLength = 6)]
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "تکرار رمز عبور")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "رمزعبور و تکرار آن باید یکی باشد.")]
        public string ConfirmPassWord { get; set; }

       

    }
}
