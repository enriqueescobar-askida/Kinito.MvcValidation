namespace MvcValidation.Services
{
    using System;
    using System.Web.Security;

    /// <summary>
    /// Class AccountMembershipService.
    /// </summary>
    public class AccountMembershipService : IMembershipService
    {
        #region Attributes
        /// <summary>
        /// The provider
        /// </summary>
        private readonly MembershipProvider provider;

        /// <summary>
        /// Gets the minimum length of the password.
        /// </summary>
        /// <value>The minimum length of the password.</value>
        public int MinPasswordLength
        {
            get
            {
                return this.provider.MinRequiredPasswordLength;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountMembershipService"/> class.
        /// </summary>
        public AccountMembershipService() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountMembershipService"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public AccountMembershipService(MembershipProvider provider)
        {
            this.provider = provider ?? Membership.Provider;
        }
        #endregion

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ValidateUser(string userName, string password)
        {
            ValidationUtility.ValidateRequiredStringValue(userName, "userName");
            ValidationUtility.ValidateRequiredStringValue(password, "password");

            return this.provider.ValidateUser(userName, password);
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="email">The email.</param>
        /// <returns>MembershipCreateStatus.</returns>
        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            ValidationUtility.ValidateRequiredStringValue(userName, "userName");
            ValidationUtility.ValidateRequiredStringValue(password, "password");
            ValidationUtility.ValidateRequiredStringValue(email, "email");

            MembershipCreateStatus status;
            this.provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            ValidationUtility.ValidateRequiredStringValue(userName, "userName");
            ValidationUtility.ValidateRequiredStringValue(oldPassword, "oldPassword");
            ValidationUtility.ValidateRequiredStringValue(newPassword, "newPassword");

            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            try
            {
                MembershipUser currentUser = this.provider.GetUser(userName, true /* userIsOnline */);
                return currentUser != null && currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentNullException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }
    }
}