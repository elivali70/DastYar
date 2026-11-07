using System.ComponentModel.DataAnnotations;

public class UpdateUserViewModel : IValidatableObject
{
    [Display(Name = "شناسه کاربر")]
    public int Id { get; set; }

    [Required]
    [Display(Name = " نام کاربری")]
    public string UserName { get; set; }

    [Display(Name = "ایمیل")]
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Display(Name = "شماره تماس")]
    public string PhoneNumber { get; set; }

    [Display(Name = "نقش‌ کاربر")]
    public List<string> Role { get; set; }

    [StringLength(maximumLength: 20, ErrorMessage = "{0}کلمه عبور حداقل {1} کارکتر و حداکثر {2} کارکتر باشد", MinimumLength = 6)]
    [Display(Name = "رمز عبور")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Display(Name = "تکرار رمز عبور")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "رمزعبور و تکرار آن باید یکی باشد.")]
    public string? ConfirmPassWord { get; set; }

    [Display(Name = "قایل")]
    public IFormFile? File {  get; set; }
    [Display(Name = "توصیح فایل")]
    public string DiscriptionFile { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrWhiteSpace(Password) && string.IsNullOrWhiteSpace(ConfirmPassWord))
        {
            yield return new ValidationResult("تکرار رمز عبور الزامی است.", new[] { nameof(ConfirmPassWord) });
        }
    }
}
