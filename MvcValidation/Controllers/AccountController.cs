namespace MvcValidation.Controllers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Security.Principal;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.Security;

    using Models;

    using MvcValidation.Services;

    using Resources.Controllers;

    /// <summary>
    /// Class AccountController.
    /// </summary>
    [HandleError]
    public class AccountController : /*BaseController*/ Controller
    {
        #region Attributes
        /// <summary>
        /// Gets the forms service.
        /// </summary>
        /// <value>The forms service.</value>
        public IFormsAuthenticationService FormsService { get; private set; }
        /// <summary>
        /// Gets the membership service.
        /// </summary>
        /// <value>The membership service.</value>
        public IMembershipService MembershipService { get; private set; }
        #endregion

        #region Constructor
        // This constructor is used by the MVC framework to instantiate the controller using
        // the default forms authentication and membership providers.
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        public AccountController()
            : this(null, null)
        {
        }

        // This constructor is not used by the MVC framework but is instead provided for ease
        // of unit testing this type. See the comments in AccountModels.cs for more information.
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="formsService">The forms service.</param>
        /// <param name="membershipService">The membership service.</param>
        public AccountController(IFormsAuthenticationService formsService, IMembershipService membershipService)
        {
            this.FormsService = formsService ?? new FormsAuthenticationService();
            this.MembershipService = membershipService ?? new AccountMembershipService();
        }
        #endregion

        #region ProtectedOverrideMethods
        /// <summary>
        /// Initializes data that might not be available when the constructor is called.
        /// </summary>
        /// <param name="requestContext">The HTTP context and route data.</param>
        /// <exception cref="InvalidOperationException">Windows authentication is not supported.</exception>
        protected override void Initialize(RequestContext requestContext)
        {
            if (requestContext.HttpContext.User.Identity is WindowsIdentity)
            {
                throw new InvalidOperationException("Windows authentication is not supported.");
            }
            base.Initialize(requestContext);
        }

        /// <summary>
        /// Called before the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewData["PasswordLength"] = this.MembershipService.MinPasswordLength;

            base.OnActionExecuting(filterContext);
        }
        #endregion

        #region ActionResultMethods
        /// <summary>
        /// Changes the culture.
        /// </summary>
        /// <param name="lang">The language.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            Session["Culture"] = new CultureInfo(lang);
            return Redirect(returnUrl);
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ActionResult.</returns>
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (this.MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                this.ModelState.AddModelError("", ErrorsCtRx.AccountController_ChangePassword_Error);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Changes the password success.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        /// <summary>
        /// Logs off.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult LogOff()
        {
            FormsService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Logs on.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult LogOn()
        {
            return View();
        }

        /// <summary>
        /// Logs the on.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Justification = "Needs to take same parameter type as Controller.Redirect()")]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (this.MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    FormsService.SignIn(model.UserName, model.RememberMe);
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return this.RedirectToAction("Index", "Home");
                }
                this.ModelState.AddModelError("", ErrorsCtRx.AccountController_LogOn_Error);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus = this.MembershipService.CreateUser(model.UserName, model.Password, model.Email);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsService.SignIn(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                this.ModelState.AddModelError("", ErrorCodeToString(createStatus));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

        /// <summary>
        /// Errors the code to string.
        /// </summary>
        /// <param name="createStatus">The create status.</param>
        /// <returns>System.String.</returns>
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return AccountCtRx.UserName + " " + ErrorsCtRx.AccountController_DuplicateUserName_Error;
                case MembershipCreateStatus.DuplicateEmail:
                    return AccountCtRx.UserName + " " + ErrorsCtRx.AccountController_DuplicateEmail_Error;
                case MembershipCreateStatus.InvalidPassword:
                    return AccountCtRx.Password + " " + ErrorsCtRx.AccountController_Invalid_Error;
                case MembershipCreateStatus.InvalidEmail:
                    return AccountCtRx.Email + " " + ErrorsCtRx.AccountController_Invalid_Error;
                case MembershipCreateStatus.InvalidAnswer:
                    return AccountCtRx.Password + " " + ErrorsCtRx.AccountController_InvalidAnswer_Error + " " + ErrorsCtRx.AccountController_Invalid_Error;
                case MembershipCreateStatus.InvalidQuestion:
                    return AccountCtRx.Password + " " + ErrorsCtRx.AccountController_InvalidQuestion_Error + " " + ErrorsCtRx.AccountController_Invalid_Error;
                case MembershipCreateStatus.InvalidUserName:
                    return AccountCtRx.UserName + " " + ErrorsCtRx.AccountController_Invalid_Error;
                case MembershipCreateStatus.ProviderError:
                    return ErrorsCtRx.AccountController_Provider_Error + " " + ErrorsCtRx.AccountController_CheckEntryAndRetry_Error;
                case MembershipCreateStatus.UserRejected:
                    return ErrorsCtRx.AccountController_UserRejected_Error + " " + ErrorsCtRx.AccountController_CheckEntryAndRetry_Error;
                default:
                    return ErrorsCtRx.AccountController_Default_Error + " " + ErrorsCtRx.AccountController_CheckEntryAndRetry_Error;
            }
        }
    }
}
