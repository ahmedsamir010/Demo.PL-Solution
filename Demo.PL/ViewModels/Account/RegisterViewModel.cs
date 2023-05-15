using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Email Is Required ")]
        [EmailAddress(ErrorMessage =" Email Is Invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required ")]
        [MinLength(5,ErrorMessage = "MinLengthis 5")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password Is Required ")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }






    }
}
