using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels.Account
{
    public class ResetPasswordViewModel
    {        
        [Required(ErrorMessage = "Password Is Required ")]
        [MinLength(5, ErrorMessage = "MinLengthis 5")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "Password Is Required ")]
        [Compare("NewPassword" , ErrorMessage ="Confirm Password doen`t math Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        
        


    }
}
