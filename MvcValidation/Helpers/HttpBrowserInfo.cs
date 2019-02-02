namespace MvcValidation.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Class HttpBrowserCulture.
    /// </summary>
    public class HttpBrowserInfo
    {
        #region Attributes
        /// <summary>
        /// Gets a value indicating whether this instance has HTTP context.
        /// </summary>
        /// <value><c>true</c> if this instance has HTTP context; otherwise, <c>false</c>.</value>
        public bool HasHttpContext { get; internal set; }

        /// <summary>
        /// Gets the HTTP browser context.
        /// </summary>
        /// <value>The HTTP browser context.</value>
        public HttpContextBase HttpBrowserContext { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance has cookie culture.
        /// </summary>
        /// <value><c>true</c> if this instance has cookie culture; otherwise, <c>false</c>.</value>
        public bool HasCookieCulture { get; internal set; }

        /// <summary>
        /// Gets the cookie culture.
        /// </summary>
        /// <value>The cookie culture.</value>
        public HttpCookie CookieCulture { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance has browser language list.
        /// </summary>
        /// <value><c>true</c> if this instance has browser language list; otherwise, <c>false</c>.</value>
        public bool HasBrowserLanguageList { get; internal set; }

        /// <summary>
        /// Gets the browser language list.
        /// </summary>
        /// <value>The browser language list.</value>
        public IList<string> BrowserLanguageList { get; internal set; }

        /// <summary>
        /// Gets the local language list.
        /// </summary>
        /// <value>The local language list.</value>
        public IList<string> LocalLanguageList { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance has cookie culture value.
        /// </summary>
        /// <value><c>true</c> if this instance has cookie culture value; otherwise, <c>false</c>.</value>
        public bool HasCookieCultureValue { get; internal set; }

        /// <summary>
        /// Gets the cookie culture string.
        /// </summary>
        /// <value>The cookie culture string.</value>
        public string CookieCultureString { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance has session culture.
        /// </summary>
        /// <value><c>true</c> if this instance has session culture; otherwise, <c>false</c>.</value>
        public bool HasSessionCulture { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance has session culture value.
        /// </summary>
        /// <value><c>true</c> if this instance has session culture value; otherwise, <c>false</c>.</value>
        public bool HasSessionCultureValue { get; internal set; }

        /// <summary>
        /// Gets the session culture string.
        /// </summary>
        /// <value>The session culture string.</value>
        public string SessionCultureString { get; internal set; }
        #endregion
        #region Constructor
        public HttpBrowserInfo(ActionExecutingContext actionExecutingContext, IList<string> localLanguageList)
            : this(actionExecutingContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpBrowserInfo"/> class.
        /// </summary>
        /// <param name="actionExecutingContext">The action executing context.</param>
        public HttpBrowserInfo(ActionExecutingContext actionExecutingContext)
        {
            this.HasHttpContext = (actionExecutingContext.RequestContext.HttpContext != null);
            this.HttpBrowserContext = actionExecutingContext.RequestContext.HttpContext;
            this.LocalLanguageList = new CanadianLanguageList().CultureLanguageList;
            this.HasBrowserLanguageList = (this.HasHttpContext
                                            && this.HttpBrowserContext.Request.UserLanguages != null);
            this.BrowserLanguageList = new List<string>(this.HttpBrowserContext.Request.UserLanguages);
            this.HasCookieCulture = (this.HttpBrowserContext != null
                                     && this.HttpBrowserContext.Request.Cookies["Culture"] != null);
            this.CookieCulture = this.HttpBrowserContext.Request.Cookies["Culture"];
            this.HasCookieCultureValue = (this.HasCookieCulture && !String.IsNullOrEmpty(this.CookieCulture.Value));
            this.CookieCultureString = this.CookieCulture.Value;
            this.HasSessionCulture = (this.HasHttpContext && this.HttpBrowserContext.Session != null
                                      && this.HttpBrowserContext.Session["Culture"] != null);
            this.HasSessionCultureValue = (this.HasSessionCulture
                                           && !String.IsNullOrEmpty(
                                               this.HttpBrowserContext.Session["Culture"].ToString()));
            this.SessionCultureString = this.HttpBrowserContext.Session["Culture"].ToString();
        }
        #endregion
        #region Destructor
        /// <summary>
        /// Finalizes an instance of the <see cref="HttpBrowserInfo"/> class.
        /// </summary>
        ~HttpBrowserInfo()
        {
            this.HasBrowserLanguageList = this.HasCookieCulture = this.HasCookieCultureValue =
                this.HasHttpContext = this.HasSessionCulture = this.HasSessionCultureValue = false;
            this.HttpBrowserContext = null;
            this.BrowserLanguageList = this.LocalLanguageList = null;
            this.CookieCulture = null;
            this.CookieCultureString = this.SessionCultureString = String.Empty;
        }
        #endregion
        #region PublicMethods
        /// <summary>
        /// Sets the cookie culture.
        /// </summary>
        /// <param name="cultureLanguage">The culture language.</param>
        /// <returns><c>true</c> if success on cookie culture creation, <c>false</c> otherwise.</returns>
        public bool SetCookieCulture(string cultureLanguage)
        {
            return this.SetCookie("Culture", cultureLanguage);
        }

        /// <summary>
        /// Sets the cookie.
        /// </summary>
        /// <param name="cookieName">Name of the cookie.</param>
        /// <param name="cookieValue">The cookie value.</param>
        /// <returns><c>true</c> if success on cookie creation, <c>false</c> otherwise.</returns>
        public bool SetCookie(string cookieName, string cookieValue)
        {
            if (String.IsNullOrEmpty(cookieName) || String.IsNullOrEmpty(cookieValue) || this.HttpBrowserContext == null)
                return false;
            
            DateTime oldDateTime = DateTime.Now.AddDays(-1);
            DateTime newDateTime = DateTime.Now.AddHours(1);
            HttpCookie httpCookie = null;
            if (this.HttpBrowserContext.Request.Cookies[cookieName] != null)
            {
                httpCookie = this.HttpBrowserContext.Request.Cookies[cookieName];
                httpCookie.Expires = oldDateTime;
                this.HttpBrowserContext.Request.Cookies.Remove(cookieName);
            }

            httpCookie = new HttpCookie(cookieName) { Value = cookieValue, Expires = newDateTime };
            this.HttpBrowserContext.Response.Cookies.Add(httpCookie);
            return true;
        }
        #endregion
    }
}