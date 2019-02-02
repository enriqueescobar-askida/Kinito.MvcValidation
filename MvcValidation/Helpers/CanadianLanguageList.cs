namespace MvcValidation.Helpers
{
    using System.Collections.Generic;

    /// <summary>
    /// Class LocalLanguageList.
    /// </summary>
    public class CanadianLanguageList
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CanadianLanguageList"/> class.
        /// </summary>
        public CanadianLanguageList()
        {
            this.CultureLanguageList = new List<string> { "fr-CA", "en-CA" };
        }
        #endregion

        #region Destructor
        /// <summary>
        /// Finalizes an instance of the <see cref="CanadianLanguageList"/> class.
        /// </summary>
        ~CanadianLanguageList()
        {
            this.CultureLanguageList = null;
        }
        #endregion

        #region Attributes
        /// <summary>
        /// Gets the local culture language list.
        /// </summary>
        /// <value>The local culture language list.</value>
        public IList<string> CultureLanguageList { get; internal set; }
        #endregion
    }
}