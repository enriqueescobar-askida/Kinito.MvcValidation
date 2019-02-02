namespace MvcValidation.Helpers
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    /// <summary>
    /// Class PropertiesMustMatchAttribute. This class cannot be inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class PropertiesMustMatchAttribute : ValidationAttribute
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertiesMustMatchAttribute"/> class.
        /// </summary>
        /// <param name="originalProperty">The original property.</param>
        /// <param name="confirmProperty">The confirm property.</param>
        public PropertiesMustMatchAttribute(string originalProperty, string confirmProperty)
            : base(errorMessage: DefaultErrorMessage)
        {
            this.OriginalProperty = originalProperty;
            this.ConfirmProperty = confirmProperty;
        }
        #endregion

        #region Attributes
        /// <summary>
        /// The default error message
        /// </summary>
        private const string DefaultErrorMessage = "'{0}' and '{1}' do not match.";

        /// <summary>
        /// The type identifier
        /// </summary>
        private readonly object typeId = new object();

        /// <summary>
        /// Gets the confirm property.
        /// </summary>
        /// <value>The confirm property.</value>
        public string ConfirmProperty
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the original property.
        /// </summary>
        /// <value>The original property.</value>
        public string OriginalProperty
        {
            get;
            private set;
        }
        #endregion

        #region PublicOverrideMethods
        /// <summary>
        /// When implemented in a derived class, gets a unique identifier for this <see cref="T:System.Attribute" />.
        /// </summary>
        /// <value>The type identifier.</value>
        public override object TypeId
        {
            get
            {
                return this.typeId;
            }
        }

        /// <summary>
        /// Applies formatting to an error message based on the data field where the error occurred.
        /// </summary>
        /// <param name="name">The name of the data field where the error occurred.</param>
        /// <returns>An instance of the formatted error message.</returns>
        public override string FormatErrorMessage(string name)
        {
            return String.Format(
                CultureInfo.CurrentUICulture,
                this.ErrorMessageString,
                this.OriginalProperty,
                this.ConfirmProperty);
        }

        /// <summary>
        /// Determines whether the specified value of the object is valid.
        /// </summary>
        /// <param name="value">The value of the specified validation object on which the <see cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute" /> is declared.</param>
        /// <returns>true if the specified value is valid; otherwise, false.</returns>
        public override bool IsValid(object value)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
            object originalValue = properties.Find(this.OriginalProperty, true /* ignoreCase */).GetValue(value);
            object confirmValue = properties.Find(this.ConfirmProperty, true /* ignoreCase */).GetValue(value);

            return Equals(originalValue, confirmValue);
        }
        #endregion
    }
}