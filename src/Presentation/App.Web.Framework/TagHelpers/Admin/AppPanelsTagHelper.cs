using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace App.Web.Framework.TagHelpers.Admin
{
    /// <summary>
    /// nop-panel tag helper
    /// </summary>
    [HtmlTargetElement("app-panels", Attributes = ID_ATTRIBUTE_NAME)]
    public class AppPanelsTagHelper : TagHelper
    {
        private const string ID_ATTRIBUTE_NAME = "id";

        /// <summary>
        /// ViewContext
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
    }
}