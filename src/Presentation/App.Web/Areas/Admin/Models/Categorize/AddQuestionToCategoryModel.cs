using App.Web.Framework.Models;
using System.Collections.Generic;

namespace App.Web.Areas.Admin.Models.Categorize
{
    /// <summary>
    /// Represents a product model to add to the category
    /// </summary>
    public partial class AddQuestionToCategoryModel : BaseModel
    {
        #region Ctor

        public AddQuestionToCategoryModel()
        {
            SelectedQuestionIds = new List<int>();
        }
        #endregion

        #region Properties

        public int CategoryId { get; set; }

        public IList<int> SelectedQuestionIds { get; set; }

        #endregion
    }
}
