using System.ComponentModel.DataAnnotations;

public class ChangePasswordViewModel
{
    [Display(Name = "شناسه کاربر")]
    public int Id { get; set; }

    [Required]
    [Display(Name = "نام کاربری")]
    public string UserName { get; set; }


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

