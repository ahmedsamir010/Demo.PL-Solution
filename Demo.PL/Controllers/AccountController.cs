using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]

        #region Register
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.Email.Split('@')[0],  //ahmed@gmail.com
                    IsAgree = registerViewModel.IsAgree

                };


                var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);

                }


            }
            return View(registerViewModel);

        }
        #endregion

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]

        #region Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

                if (user != null)
                {
                    var passwordIsValid = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

                    if (passwordIsValid)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }

            }
            return View(loginViewModel);


        }



        #endregion

        #region SignOut

        public  async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
             
            return RedirectToAction("Login", "Account");
        }



        #endregion


        #region ForgetPassword
        public IActionResult ForgetPassword()
        {
            return View();
        }

        #endregion

        #region SendEmail
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgetPasswordViewModel.Email);

                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetpasswordLink = Url.Action("ResetPassword", "Account", new { Email = forgetPasswordViewModel.Email, Token = token, }, Request.Scheme);
                    var email = new Email()
                    {
                        Title = "Reset Password",
                        Body = resetpasswordLink,
                        To = forgetPasswordViewModel.Email
                    };

                    // To Send Email 

                    EmailSettings.SendEmail(email);


                    return RedirectToAction("CheckYourInbox");

                }
                ModelState.AddModelError(string.Empty, "Invalid Email.");



            }

            return View();
        }


        #endregion

        #region CheckYourInbox

        public IActionResult CheckYourInbox()
        {

            return View();
        } 
        #endregion


        public IActionResult ResetPassword(string email,string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;


            return View();
        }

        #region ResetPassword

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                string email = TempData["email"] as string;
                string token = TempData["token"] as string;


                var user = await _userManager.FindByEmailAsync(email);

                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, token, resetPasswordViewModel.NewPassword);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Login));
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                }


            }
            return View(resetPasswordViewModel);
        }
        #endregion

        #region ResetPasswordDone
        public IActionResult ResetPasswordDone()
        {
            return View();
        } 
        #endregion
   
        
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
