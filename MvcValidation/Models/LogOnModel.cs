namespace MvcValidation.Models
{
    using System.ComponentModel.DataAnnotations;

    using MvcValidation.Constants;
    using MvcValidation.Resources.Models;

    /// <summary>
    /// Class LogOnModel.
    /// </summary>
    public class LogOnModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationStringsMdRx))]
        //[Required(ErrorMessageResourceType = typeof(ErrorsMdRx), ErrorMessageResourceName = "UserName_Error")]
        [LocalizedDisplayName("UserName", typeof(LogOnMdRx))]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationStringsMdRx))]
        //[Required(ErrorMessageResourceType = typeof(ErrorsMdRx), ErrorMessageResourceName = "Password_Error")]
        [DataType(DataType.Password)]
        [LocalizedDisplayName("Password", typeof(LogOnMdRx))]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remember me].
        /// </summary>
        /// <value><c>true</c> if [remember me]; otherwise, <c>false</c>.</value>
        [LocalizedDisplayName("RememberMe", typeof(LogOnMdRx))]
        public bool RememberMe { get; set; }
    }
}