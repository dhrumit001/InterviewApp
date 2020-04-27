using Microsoft.AspNetCore.Mvc;

namespace App.Web.Framework.Controllers
{
    public class BaseController : Controller
    {
        #region Security

        /// <summary>
        /// Access denied view
        /// </summary>
        /// <returns>Access denied view</returns>
        protected virtual IActionResult AccessDeniedView()
        {
            //var webHelper = EngineContext.Current.Resolve<IWebHelper>();

            //return Challenge();
            //return RedirectToAction("AccessDenied", "Security", new { pageUrl = webHelper.GetRawUrl(Request) });
            return RedirectToAction("AccessDenied", "Security", new { area = "admin" });
        }

        #endregion
    }
}
