using App.Web.Framework.Models;

namespace App.Web.Models.Categorize
{
    /// <summary>
    /// Represents a category model
    /// </summary>
    public partial class CategoryModel : BaseEntityModel
    {
        #region Properties

        public string Name { get; set; }

        public string Description { get; set; }

        public int ParentCategoryId { get; set; }

        public int DisplayOrder { get; set; }

        #endregion
    }
}
