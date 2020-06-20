using App.Web.Areas.Admin.Models.Cms;

namespace App.Web.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the widget model factory
    /// </summary>
    public partial interface IWidgetModelFactory
    {
        /// <summary>
        /// Prepare widget search model
        /// </summary>
        /// <param name="searchModel">Widget search model</param>
        /// <returns>Widget search model</returns>
        WidgetSearchModel PrepareWidgetSearchModel(WidgetSearchModel searchModel);
    }
}
