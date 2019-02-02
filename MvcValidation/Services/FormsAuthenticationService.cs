namespace MvcValidation.Services
{
    using System.Web.Security;

    /// <summary>
    /// Class FormsAuthenticationService.
    /// </summary>
    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        /// <summary>
        /// Signs in.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="createPersistentCookie">if set to <c>true</c> [create persistent cookie].</param>
        public void SignIn(string userName, bool createPersistentCookie)
        {
            ValidationUtility.ValidateRequiredStringValue(userName, "userName");

            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        /// <summary>
        /// Signs out.
        /// </summary>
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}