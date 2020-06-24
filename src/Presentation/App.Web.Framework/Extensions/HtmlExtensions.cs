using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Text.Encodings.Web;

namespace App.Web.Framework.Extensions
{
    public static class HtmlExtensions
    {
        #region Common extensions

        /// <summary>
        /// Convert IHtmlContent to string
        /// </summary>
        /// <param name="htmlContent">HTML content</param>
        /// <returns>Result</returns>
        public static string RenderHtmlContent(this IHtmlContent htmlContent)
        {
            using (var writer = new StringWriter())
            {
                htmlContent.WriteTo(writer, HtmlEncoder.Default);
                var htmlOutput = writer.ToString();
                return htmlOutput;
            }
        }

        /// <summary>
        /// Convert IHtmlContent to string
        /// </summary>
        /// <param name="tag">Tag</param>
        /// <returns>String</returns>
        public static string ToHtmlString(this IHtmlContent tag)
        {
            using (var writer = new StringWriter())
            {
                tag.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }

        /// <summary>
        /// Gets a selected tab name (used in admin area to store selected tab name)
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="dataKeyPrefix">Key prefix. Pass null to ignore</param>
        /// <returns>Name</returns>
        public static string GetSelectedTabName(this IHtmlHelper helper, string dataKeyPrefix = null)
        {
            //keep this method synchronized with
            //"SaveSelectedTab" method of \Area\Admin\Controllers\BaseAdminController.cs
            var tabName = string.Empty;
            var dataKey = "nop.selected-tab-name";
            if (!string.IsNullOrEmpty(dataKeyPrefix))
                dataKey += $"-{dataKeyPrefix}";

            if (helper.ViewData.ContainsKey(dataKey))
                tabName = helper.ViewData[dataKey].ToString();

            if (helper.ViewContext.TempData.ContainsKey(dataKey))
                tabName = helper.ViewContext.TempData[dataKey].ToString();

            return tabName;
        }

        #endregion
    }
}
