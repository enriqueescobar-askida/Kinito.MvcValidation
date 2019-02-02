namespace MvcValidation.Services
{
    using System;

    /// <summary>
    /// Class ValidationUtility.
    /// </summary>
    public static class ValidationUtility
    {
        /// <summary>
        /// The _string required error message
        /// </summary>
        private const string StringRequiredErrorMessage = "Value cannot be null or empty.";

        /// <summary>
        /// Validates the required string value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void ValidateRequiredStringValue(string value, string parameterName)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException(StringRequiredErrorMessage, parameterName);
        }
    }
}