using App.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.Web.Areas.Admin.Models.Categorize
{
    /// <summary>
    /// Represents a category model
    /// </summary>
    public partial class CategoryModel : BaseEntityModel
    {
        #region Ctor

        public CategoryModel()
        {
            AvailableCategories = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        [DisplayName("Category name")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Parent category")]
        public int ParentCategoryId { get; set; }

        [UIHint("Picture")]
        [DisplayName("Picture")]
        public int PictureId { get; set; }

        [DisplayName("Published")]
        public bool Published { get; set; }

        [DisplayName("Deleted")]
        public bool Deleted { get; set; }

        [DisplayName("Display order")]
        public int DisplayOrder { get; set; }
        public string Breadcrumb { get; set; }

        public IList<SelectListItem> AvailableCategories { get; set; }

        #endregion
    }

}
