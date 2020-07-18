using App.Core.Domain.Users;
using App.Services.Authentication;
using App.Services.Users;
using App.Web.Areas.Admin.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        #region Fields
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRegistrationService _userRegistrationService;
        #endregion

        #region Ctor

        public AccountController(IUserService userService,
            IAuthenticationService authenticationService,
            IUserRegistrationService userRegistrationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _userRegistrationService = userRegistrationService;
        }

        #endregion

        #region Utilities

        #endregion

        #region Methods

        #region Login / logout

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public virtual IActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                model.Email = model.Email.Trim();
                var loginResult = _userRegistrationService.ValidateUser(model.Email, model.Password);
                switch (loginResult)
                {
                    case UserLoginResults.Successful:
                        {
                            var customer = _userService.GetUserByUsername(model.Email);

                            //sign in new customer
                            _authenticationService.SignIn(customer, model.RememberMe);

                            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                                return RedirectToAction("Index", "Home");

                            return Redirect(returnUrl);
                        }
                    case UserLoginResults.UserNotExist:
                        ModelState.AddModelError("", "No user account found");
                        break;
                    case UserLoginResults.Deleted:
                        ModelState.AddModelError("", "User is deleted");
                        break;
                    case UserLoginResults.NotActive:
                        ModelState.AddModelError("", "Account is not active");
                        break;
                    case UserLoginResults.NotRegistered:
                        ModelState.AddModelError("", "Account is not registered");
                        break;
                    case UserLoginResults.LockedOut:
                        ModelState.AddModelError("", "User is locked out");
                        break;
                    case UserLoginResults.WrongPassword:
                    default:
                        ModelState.AddModelError("", "The credentials provided are incorrect");
                        break;
                }
            }

            return View(model);
        }

        public virtual IActionResult Logout()
        {
            //standard logout 
            _authenticationService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #endregion
    }
}