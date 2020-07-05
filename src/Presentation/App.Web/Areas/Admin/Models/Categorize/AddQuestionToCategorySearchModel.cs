using App.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Areas.Admin.Models.Categorize
{
    /// <summary>
    /// Represents a product search model to add to the category
    /// </summary>
    public partial class AddQuestionToCategorySearchModel : BaseSearchModel
    {
        #region Ctor

        public AddQuestionToCategorySearchModel()
        {
            AvailableCategories = new List<SelectListItem>();
            AvailableQuestionTypes = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        [DisplayName("Question name")]
        public string SearchQuestionName { get; set; }

        [DisplayName("Category")]
        public int SearchCategoryId { get; set; }

        [DisplayName("Question type")]
        public int SearchQuestionTypeId { get; set; }

        public IList<SelectListItem> AvailableCategories { get; set; }

        public IList<SelectListItem> AvailableQuestionTypes { get; set; }

        #endregion
    }
}
