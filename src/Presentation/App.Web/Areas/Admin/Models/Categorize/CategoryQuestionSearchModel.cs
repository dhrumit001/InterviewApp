using App.Web.Framework.Models;

namespace App.Web.Areas.Admin.Models.Categorize
{
    /// <summary>
    /// Represents a category product search model
    /// </summary>
    public partial class CategoryQuestionSearchModel : BaseSearchModel
    {
        #region Properties

        public int CategoryId { get; set; }

        #endregion
    }
}
