using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eMarket.Models
{
    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
    
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "ErrorMessage.RequiredEmail")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "ErrorMessage.RequiredPassword")]
        [StringLength(100, ErrorMessage = "ErrorMessage.StringLengthPassword", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ErrorMessage.RequiredConfirmPassword")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password.")]
        [Compare("Password", ErrorMessage = "ErrorMessage.ComparePassword")]
        public string ConfirmPassword { get; set; }

       // [Required(ErrorMessage = "ErrorMessage.RequiredCaptcha")]
        [Display(Name = "Captcha")]
        public string Captcha { get; set; }
    }
}
