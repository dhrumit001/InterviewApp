using App.Web.Framework.Models;

namespace App.Web.Areas.Admin.Models.Cms
{
    public partial class RenderWidgetModel : BaseModel
    {
        public string WidgetViewComponentName { get; set; }
        public object WidgetViewComponentArguments { get; set; }
    }
}