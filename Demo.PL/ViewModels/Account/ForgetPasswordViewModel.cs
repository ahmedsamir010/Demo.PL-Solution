using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels.Account
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email Is Required ")]
        [EmailAddress(ErrorMessage = " Email Is Invalid")]
        public string Email { get; set; }

    }
}
    