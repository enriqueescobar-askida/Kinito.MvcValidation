namespace MvcValidation.Constants
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    /// <summary>
    /// Class LocalizedDisplayName.
    /// </summary>
    public class LocalizedDisplayName : DisplayNameAttribute
    {
        #region Attributes
        /// <summary>
        /// The name property
        /// </summary>
        private /*readonly*/ PropertyInfo NameProperty;

        /// <summary>
        /// The resource type
        /// </summary>
        private Type ResourceType;
        #endregion

        #region Accessors
        public Type NameResourceType
        {
            get
            {
                return this.ResourceType;
            }
            set
            {
                this.ResourceType = value;

                // initialize nameProperty when type property is provided by setter
                this.NameProperty = this.ResourceType.GetProperty(base.DisplayName, 
                                                            BindingFlags.Static | BindingFlags.Public);
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedDisplayName"/> class.
        /// </summary>
        /// <param name="displayNameKey">The display name key.</param>
        /// <param name="resourceType">Type of the resource.</param>
        public LocalizedDisplayName(string displayNameKey, Type resourceType = null)
            : base(displayNameKey)
        {
            if (resourceType != null)
                this.NameProperty = resourceType.GetProperty(base.DisplayName,
                                                            BindingFlags.Static | BindingFlags.Public);
        }

        /// <summary>
        /// Gets the display name for a property, event, or public void method that takes no arguments stored in this attribute.
        /// </summary>
        /// <value>The display name.</value>
        public override string DisplayName
        {
            get
            {
                // check if nameProperty is null and return original display name value
                if (this.NameProperty == null)
                    return base.DisplayName;

                return (string)this.NameProperty.GetValue(this.NameProperty.DeclaringType, null);
            }
        }
    }
}