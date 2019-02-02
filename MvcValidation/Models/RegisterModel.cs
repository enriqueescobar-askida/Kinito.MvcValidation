namespace MvcValidation.Models
{
    using System.ComponentModel.DataAnnotations;

    using MvcValidation.Helpers;
    using MvcValidation.Constants;
    using MvcValidation.Resources.Models;

    /// <summary>
    /// Class RegisterModel.
    /// </summary>
    [PropertiesMustMatch("Password",
                        "ConfirmPassword",
                        ErrorMessageResourceName = "PasswordsMustMatch",
                        ErrorMessageResourceType = typeof(ValidationStringsMdRx))]
        //ErrorMessage = "The password and confirmation password do not match.")]
    public class RegisterModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationStringsMdRx))]
        //[Required(ErrorMessageResourceType = typeof(ErrorsMdRx), ErrorMessageResourceName = "UserName_Error")]
        [LocalizedDisplayName("UserName", typeof(RegisterMdRx))]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationStringsMdRx))]
        //[Required(ErrorMessageResourceType = typeof(ErrorsMdRx), ErrorMessageResourceName = "Email_Error")]
        [DataType(DataType.EmailAddress)]
        [LocalizedDisplayName("Email", typeof(RegisterMdRx))]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationStringsMdRx))]
        //[Required(ErrorMessageResourceType = typeof(ErrorsMdRx), ErrorMessageResourceName = "Password_Error")]
        [DataType(DataType.Password)]
        [LocalizedDisplayName("Password", typeof(RegisterMdRx))]
        //[ValidatePasswordLength]
        [ValidatePasswordLength(ErrorMessageResourceName = "PasswordMinLength", ErrorMessageResourceType = typeof(ValidationStringsMdRx))]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        /// <value>The confirm password.</value>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationStringsMdRx))]
        //[Required(ErrorMessageResourceType = typeof(ErrorsMdRx), ErrorMessageResourceName = "ConfirmPassword_Error")]
        [DataType(DataType.Password)]
        [LocalizedDisplayName("ConfirmPassword", typeof(RegisterMdRx))]
        public string ConfirmPassword { get; set; }
    }
}