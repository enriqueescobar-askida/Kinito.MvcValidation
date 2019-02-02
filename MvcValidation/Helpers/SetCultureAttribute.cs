namespace MvcValidation.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Class SetCultureAttribute.
    /// </summary>
    public class SetCultureAttribute : FilterAttribute, IActionFilter
    {
        #region PrivateStaticMethods
        /// <summary>
        /// Finds the cookie culture value in language list.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        /// <param name="cultureLanguageList">The culture language list.</param>
        /// <returns>System.String.</returns>
        private static string FindCookieCultureValueInLanguageList(ActionExecutingContext filterContext,
                                                IList<string> cultureLanguageList)
        {
            /* Get the language in the cookie*/
            //HttpCookie userCookie = filterContext.RequestContext.HttpContext.Request.Cookies["Culture"];
            HttpBrowserInfo httpBrowserInfo = new HttpBrowserInfo(filterContext);//, cultureLanguageList);
            return (cultureLanguageList.Contains(httpBrowserInfo.CookieCultureString))
                    ? httpBrowserInfo.CookieCultureString
                    : String.Empty;
            /*string s = String.Empty;
            if (cultureLanguageList.Contains(httpBrowserInfo.CookieCultureString))
            {
                s = httpBrowserInfo.CookieCultureString;
            }
            return s;*/
            /*if (userCookie != null)
            {
                if (!String.IsNullOrEmpty(userCookie.Value))
                {
                    if (cultureLanguageList.Contains(userCookie.Value))
                    {
                        return userCookie.Value;
                    }
                    return String.Empty;
                }
                return String.Empty;
            }
            return String.Empty;*/
        }

        /// <summary>
        /// Finds the first browser culture list in language list.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        /// <param name="cultureLanguageList">The culture language list.</param>
        /// <returns>System.String.</returns>
        private static string FindFirstBrowserCultureListInLanguageList(ActionExecutingContext filterContext,
                                                IList<string> cultureLanguageList)
        {
            string s = String.Empty;
            /* Gets Languages from Browser */
            //IList<string> cultureLanguageList = cultureEnumerable.ToList();
            ////IList<string> browserLanguageList = filterContext.RequestContext.HttpContext.Request.UserLanguages;
            HttpBrowserInfo httpBrowserInfo = new HttpBrowserInfo(filterContext);//, cultureLanguageList);

            if (/*browserLanguageList*/ httpBrowserInfo.BrowserLanguageList != null)
            {
                foreach (string browserLanguage in /*browserLanguageList*/ httpBrowserInfo.BrowserLanguageList)
                {
                    foreach (string cultureLanguage in cultureLanguageList)
                    {
                        if (cultureLanguage != browserLanguage)
                            continue;
                        s = cultureLanguage;
                    }
                }
            }
            return s;
        }

        /// <summary>
        /// Finds the session culture value in language list.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        /// <param name="cultureLanguageList">The culture language list.</param>
        /// <returns>System.String.</returns>
        private static string FindSessionCultureValueInLanguageList(ActionExecutingContext filterContext,
                                                IList<string> cultureLanguageList)
        {
            HttpBrowserInfo httpBrowserInfo = new HttpBrowserInfo(filterContext);//, cultureLanguageList);
            return (httpBrowserInfo.HasSessionCultureValue &&
                        cultureLanguageList.Contains(httpBrowserInfo.SessionCultureString))
                        ? httpBrowserInfo.SessionCultureString : String.Empty;
            /*string s = String.Empty;
            HttpBrowserInfo httpBrowserInfo = new HttpBrowserInfo(filterContext, cultureLanguageList);
            if (!String.IsNullOrEmpty(httpBrowserInfo.SessionCultureString))
            {
                s = cultureLanguageList.Contains(httpBrowserInfo.SessionCultureString) ? httpBrowserInfo.SessionCultureString : String.Empty;
            }
            return s;*/
            /*
            if (filterContext.RequestContext.HttpContext.Session != null && filterContext.RequestContext.HttpContext.Session["Culture"] != null)
            {
                string sessionCulture = filterContext.RequestContext.HttpContext.Session["Culture"].ToString();

                if (!String.IsNullOrEmpty(sessionCulture))
                {
                    return cultureLanguageList.Contains(sessionCulture)
                                                     ? sessionCulture
                                                     : String.Empty;
                }
                return String.Empty;
            }
            return String.Empty;*/
        }

        /// <summary>
        /// Sets the current language.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        /// <returns>System.String.</returns>
        private static string SetCurrentLanguage(ActionExecutingContext filterContext)
        {
            IList<string> cultureLanguageList = new CanadianLanguageList().CultureLanguageList;
            string cookieValue = FindCookieCultureValueInLanguageList(filterContext, cultureLanguageList);

            if (String.IsNullOrEmpty(cookieValue))
            {
                string sessionValue = FindSessionCultureValueInLanguageList(filterContext, cultureLanguageList);

                if (string.IsNullOrEmpty(sessionValue))
                {
                    string browserCulture = FindFirstBrowserCultureListInLanguageList(filterContext, cultureLanguageList);
                    return string.IsNullOrEmpty(browserCulture) ? "en-CA" : browserCulture;
                }
                return sessionValue;
            }
            return cookieValue;
        }
        #endregion

        #region PublicMethods
        /// <summary>
        /// Called before an action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string cultureCode = SetCurrentLanguage(filterContext);

            if (String.IsNullOrEmpty(cultureCode)) return;
            if (filterContext == null) return;

            HttpContext.Current.Response.Cookies.Add(
                new HttpCookie("Culture", cultureCode)
                    {
                        HttpOnly = true,
                        Expires = DateTime.Now.AddYears(100)
                    }
            );

            if (filterContext.HttpContext.Session != null)
            {
                filterContext.HttpContext.Session["Culture"] = cultureCode;
            }

            CultureInfo cultureInfo = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            //Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
        }

        #region PublicEmptyMethods
        /// <summary>
        /// Called after the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
        #endregion
        #endregion
    }
}