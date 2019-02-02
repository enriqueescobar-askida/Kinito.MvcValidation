namespace MvcValidation.Services
{
    /// <summary>
    /// Interface IFormsAuthenticationService
    /// </summary>
    public interface IFormsAuthenticationService
    {
        /// <summary>
        /// Signs in.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="createPersistentCookie">if set to <c>true</c> [create persistent cookie].</param>
        void SignIn(string userName, bool createPersistentCookie);

        /// <summary>
        /// Signs out.
        /// </summary>
        void SignOut();
    }
}