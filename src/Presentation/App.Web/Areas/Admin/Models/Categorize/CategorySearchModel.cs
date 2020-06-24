using App.Web.Framework.Models;
using System.ComponentModel;

namespace App.Web.Areas.Admin.Models.Categorize
{
    /// <summary>
    /// Represents a category search model
    /// </summary>
    public partial class CategorySearchModel : BaseSearchModel
    {
        #region Ctor

        public CategorySearchModel()
        {
        }

        #endregion

        #region Properties

        [DisplayName("Category name")]
        public string SearchCategoryName { get; set; }

        #endregion
    }
}
