namespace MvcValidation.Controllers
{
    using System.Web.Mvc;

    using Resources.Controllers;

    /// <summary>
    /// Class HomeController.
    /// </summary>
    [HandleError]
    public class HomeController : /*BaseController*/ Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        //[OutputCache(Duration = 3600, VaryByParam = "none", VaryByCustom = "en")]
        public ActionResult Index()
        {
            ViewData["Message"] = HomeCtRx.WelcomeMessage;
            return View();
        }

        /// <summary>
        /// Abouts this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult About()
        {
            return View();
        }
    }
}
