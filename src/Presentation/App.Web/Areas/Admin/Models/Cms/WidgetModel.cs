using App.Web.Framework.Models;
using Microsoft.AspNetCore.Routing;

namespace App.Web.Areas.Admin.Models.Cms
{
    /// <summary>
    /// Represents a widget model
    /// </summary>
    public partial class WidgetModel : BaseModel
    {
        #region Properties
        public string FriendlyName { get; set; }
        public string SystemName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public string ConfigurationUrl { get; set; }
        public string LogoUrl { get; set; }
        public string WidgetViewComponentName { get; set; }

        public RouteValueDictionary WidgetViewComponentArguments { get; set; }

        #endregion
    }
}