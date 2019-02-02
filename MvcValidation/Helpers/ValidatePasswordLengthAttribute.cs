namespace MvcValidation.Helpers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Web.Security;

    /// <summary>
    /// Class ValidatePasswordLengthAttribute. This class cannot be inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property,
                    AllowMultiple = false,
                    Inherited = true)]
    public sealed class ValidatePasswordLengthAttribute
        : ValidationAttribute
    {
        #region Attributes
        /// <summary>
        /// The default error message
        /// </summary>
        private const string DefaultErrorMessage = "'{0}' must be at least {1} characters long.";

        /// <summary>
        /// The minimum characters
        /// </summary>
        private readonly int minCharacters = Membership.Provider.MinRequiredPasswordLength;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatePasswordLengthAttribute"/> class.
        /// </summary>
        public ValidatePasswordLengthAttribute()
            : base(DefaultErrorMessage)
        {
        }
        #endregion

        #region PublicOverrideMethods
        /// <summary>
        /// Applies formatting to an error message based on the data field where the error occurred.
        /// </summary>
        /// <param name="name">The name of the data field where the error occurred.</param>
        /// <returns>An instance of the formatted error message.</returns>
        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentUICulture,
                                this.ErrorMessageString,
                                name,
                                this.minCharacters);
        }

        /// <summary>
        /// Determines whether the specified value of the object is valid.
        /// </summary>
        /// <param name="value">The value of the specified validation object on which the <see cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute" /> is declared.</param>
        /// <returns>true if the specified value is valid; otherwise, false.</returns>
        public override bool IsValid(object value)
        {
            string valueAsString = value as string;
            return (valueAsString != null &&
                    valueAsString.Length >= this.minCharacters);
        }
        #endregion
    }
}