using App.Web.Framework.Models;
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

        #region DataTables

        /// <summary>
        /// Creates an object that serializes the specified object to JSON
        /// Used to serialize data for DataTables
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="model">The model to serialize.</param>
        /// <returns>The created object that serializes the specified data to JSON format for the response.</returns>
        public JsonResult Json<T>(BasePagedListModel<T> model) where T : BaseModel
        {
            return Json(new
            {
                draw = model.Draw,
                recordsTotal = model.RecordsTotal,
                recordsFiltered = model.RecordsFiltered,
                data = model.Data,
            });
        }

        #endregion
    }
}
