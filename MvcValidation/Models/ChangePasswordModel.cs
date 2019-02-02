namespace MvcValidation.Models
{
    using System.ComponentModel.DataAnnotations;

    using MvcValidation.Constants;
    using MvcValidation.Helpers;
    using MvcValidation.Resources.Models;

    /// <summary>
    /// Class ChangePasswordModel.
    /// </summary>
    [PropertiesMustMatch("NewPassword", "ConfirmPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    public class ChangePasswordModel
    {
        /// <summary>
        /// Gets or sets the old password.
        /// </summary>
        /// <value>The old password.</value>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationStringsMdRx))]
        //[Required(ErrorMessageResourceType = typeof(ErrorsMdRx), ErrorMessageResourceName = "CurrentPassword_Error")]
        [DataType(DataType.Password)]
        [LocalizedDisplayName("CurrentPassword", typeof(ChangePasswordMdRx))]
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        /// <value>The new password.</value>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationStringsMdRx))]
        //[Required(ErrorMessageResourceType = typeof(ErrorsMdRx), ErrorMessageResourceName = "NewPassword_Error")]
        [DataType(DataType.Password)]
        [LocalizedDisplayName("NewPassword", typeof(ChangePasswordMdRx))]
        [ValidatePasswordLength]
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        /// <value>The confirm password.</value>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationStringsMdRx))]
        //[Required(ErrorMessageResourceType = typeof(ErrorsMdRx), ErrorMessageResourceName = "ConfirmNewPassword_Error")]
        [DataType(DataType.Password)]
        [LocalizedDisplayName("ConfirmNewPassword", typeof(ChangePasswordMdRx))]
        public string ConfirmPassword { get; set; }
    }
}