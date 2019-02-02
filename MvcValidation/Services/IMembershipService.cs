namespace MvcValidation.Services
{
    using System.Web.Security;

    /// <summary>
    /// Interface IMembershipService
    /// </summary>
    public interface IMembershipService
    {
        #region Attributes
        /// <summary>
        /// Gets the minimum length of the password.
        /// </summary>
        /// <value>The minimum length of the password.</value>
        int MinPasswordLength { get; }
        #endregion

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool ValidateUser(string userName, string password);

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="email">The email.</param>
        /// <returns>MembershipCreateStatus.</returns>
        MembershipCreateStatus CreateUser(string userName, string password, string email);

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }
}