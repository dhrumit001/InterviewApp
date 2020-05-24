using App.Web.Framework.Controllers;
using App.Web.Framework.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace App.Web.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    [Area("Admin")]
    public class BaseAdminController : BaseController
    {
        /// <summary>
        /// Creates an object that serializes the specified object to JSON.
        /// </summary>
        /// <param name="data">The object to serialize.</param>
        /// <returns>The created object that serializes the specified data to JSON format for the response.</returns>
        public override JsonResult Json(object data)
        {
            //use IsoDateFormat on writing JSON text to fix issue with dates in grid
            var useIsoDateFormat = false;
            var serializerSettings = new JsonSerializerSettings();

            if (!useIsoDateFormat)
                return base.Json(data, serializerSettings);

            serializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            serializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Unspecified;

            return base.Json(data, serializerSettings);
        }
    }
}
