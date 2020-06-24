using System;
using System.Collections.Generic;
using App.Web.Framework.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace App.Web.Framework.TagHelpers.Admin
{
    /// <summary>
    /// "nop-panel tag helper
    /// </summary>
    [HtmlTargetElement("app-panel", Attributes = NAME_ATTRIBUTE_NAME)]
    public class AppPanelTagHelper : TagHelper
    {
        private const string NAME_ATTRIBUTE_NAME = "asp-name";
        private const string TITLE_ATTRIBUTE_NAME = "asp-title";
        private const string PANEL_ICON_ATTRIBUTE_NAME = "asp-icon";

        private readonly IHtmlHelper _htmlHelper;

        /// <summary>
        /// Title of the panel
        /// </summary>
        [HtmlAttributeName(TITLE_ATTRIBUTE_NAME)]
        public string Title { get; set; }

        /// <summary>
        /// Name of the panel
        /// </summary>
        [HtmlAttributeName(NAME_ATTRIBUTE_NAME)]
        public string Name { get; set; }

        /// <summary>
        /// Panel icon
        /// </summary>
        [HtmlAttributeName(PANEL_ICON_ATTRIBUTE_NAME)]
        public string PanelIconIsAdvanced { get; set; }

        /// <summary>
        /// ViewContext
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="htmlHelper">HTML helper</param>
        public AppPanelTagHelper(IHtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="output">Output</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            //contextualize IHtmlHelper
            var viewContextAware = _htmlHelper as IViewContextAware;
            viewContextAware?.Contextualize(ViewContext);

            //create panel
            var panel = new TagBuilder("div")
            {
                Attributes =
                {
                    new KeyValuePair<string, string>("id", Name),
                    new KeyValuePair<string, string>("data-panel-name", Name),
                }
            };
            panel.AddCssClass("panel panel-default collapsible-panel");

            //create panel heading and append title and icon to it
            var panelHeading = new TagBuilder("div");
            panelHeading.AddCssClass("panel-heading");

            if (context.AllAttributes.ContainsName(PANEL_ICON_ATTRIBUTE_NAME))
            {
                var panelIcon = new TagBuilder("i");
                panelIcon.AddCssClass("panel-icon");
                panelIcon.AddCssClass(context.AllAttributes[PANEL_ICON_ATTRIBUTE_NAME].Value.ToString());
                var iconContainer = new TagBuilder("div");
                iconContainer.AddCssClass("icon-container");
                iconContainer.InnerHtml.AppendHtml(panelIcon);
                panelHeading.InnerHtml.AppendHtml(iconContainer);
            }

            panelHeading.InnerHtml.AppendHtml($"<span>{context.AllAttributes[TITLE_ATTRIBUTE_NAME].Value}</span>");

            var collapseIcon = new TagBuilder("i");
            collapseIcon.AddCssClass("fa");
            collapseIcon.AddCssClass("toggle-icon");
            panelHeading.InnerHtml.AppendHtml(collapseIcon);

            //create inner panel container to toggle on click and add data to it
            var panelContainer = new TagBuilder("div");
            panelContainer.AddCssClass("panel-container");

            var k = output.GetChildContentAsync();
            var k1 = output.GetChildContentAsync().Result;
            var k2 = output.GetChildContentAsync().Result.GetContent();
            panelContainer.InnerHtml.AppendHtml(output.GetChildContentAsync().Result.GetContent());

            //add heading and container to panel
            panel.InnerHtml.AppendHtml(panelHeading);
            panel.InnerHtml.AppendHtml(panelContainer);

            output.Content.AppendHtml(panel.RenderHtmlContent());
        }
    }
}