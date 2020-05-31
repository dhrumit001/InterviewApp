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

        /// <summary>
        /// Access denied JSON data for DataTables
        /// </summary>
        /// <returns>Access denied JSON data</returns>
        protected JsonResult AccessDeniedDataTablesJson()
        {
            return ErrorJson("You do not have permission to perform the selected operation.");
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

        #region Notifications

        /// <summary>
        /// Error's JSON data
        /// </summary>
        /// <param name="error">Error text</param>
        /// <returns>Error's JSON data</returns>
        protected JsonResult ErrorJson(string error)
        {
            return Json(new
            {
                error = error
            });
        }

        /// <summary>
        /// Error's JSON data
        /// </summary>
        /// <param name="errors">Error messages</param>
        /// <returns>Error's JSON data</returns>
        protected JsonResult ErrorJson(object errors)
        {
            return Json(new
            {
                error = errors
            });
        }

        #endregion
    }
}
