namespace MvcValidation
{
    using System;
    using System.Configuration;
    using System.Threading;
    using System.Globalization;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using MvcValidation.Helpers;

    /// For instructions on enabling IIS6 or IIS7 classic mode, visit http://go.microsoft.com/?LinkId=9394801

    /// <summary>
    /// Class MvcApplication.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        #region PublicStaticMethods
        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );
        }
        #endregion

        #region Public OverrideMethods
        /// <summary>
        /// Gets the vary by custom string.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public override string GetVaryByCustomString(HttpContext context, string value)
        {
            string s;
            string appSettingsLanguage = ConfigurationManager.AppSettings["Language"].ToLowerInvariant();
            CultureInfo appSettingsCultureInfo = new CultureInfo(appSettingsLanguage);

            string thisThreadLanguage =
                Thread.CurrentThread.CurrentUICulture.Name.ToLowerInvariant().Substring(0, 2);
            CultureInfo thisThreadCultureInfo = new CultureInfo(thisThreadLanguage);

            string baseThreadLanguage = (base.GetVaryByCustomString(context, value) == null)
                                            ? null
                                            : base.GetVaryByCustomString(context, value).ToLowerInvariant();
            CultureInfo baseThreadCultureInfo = (String.IsNullOrEmpty(baseThreadLanguage))
                                                    ? null
                                                    : new CultureInfo(baseThreadLanguage);

            Thread.CurrentThread.CurrentUICulture = appSettingsCultureInfo;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(appSettingsCultureInfo.Name);

            if (thisThreadLanguage.Contains(appSettingsLanguage))
            {
                s = appSettingsLanguage;
            }
            //if (!thisThreadLanguage.Equals(appSettingsLanguage))
            //{
            //    return appSettingsLanguage;
            //}
            s = appSettingsLanguage; // baseThreadLanguage;
            this.Session["Culture"] = appSettingsCultureInfo;
            return s;
        }
        #endregion

        #region ProtectedMethods
        /// <summary>
        /// Application_s the start.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
        }

        /// <summary>
        /// Handles the AcquireRequestState event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            // Create culture info object 
            // ...

            //It's important to check whether session object is ready
            if (HttpContext.Current.Session != null)
            {
                //Create culture info object
                CultureInfo cultureInfo;

                //Checking first if there is no value in session and set default language
                string sessionCulture = (this.Session["Culture"] == null)
                                    ? String.Empty
                                    : this.Session["Culture"].ToString().ToLowerInvariant() ;
                //cultureInfo = (CultureInfo)sessionCulture;
                cultureInfo = new CultureInfo(sessionCulture);
                // this can happen for first user's request
                if (String.IsNullOrEmpty(sessionCulture))
                {
                    //Sets default culture to English invariant
                    string langName = "en";
                    //Try to get values from Accept language HTTP header
                    if (HttpContext.Current.Request.UserLanguages != null
                        && HttpContext.Current.Request.UserLanguages.Length != 0)
                    {
                        //Gets accepted list
                        langName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);
                    }
                    cultureInfo = new CultureInfo(langName);
                    this.Session["Culture"] = cultureInfo;
                }
                //Finally setting culture for each request
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
            }
        }
        #endregion
    }
}