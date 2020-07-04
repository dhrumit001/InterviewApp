using App.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;

namespace App.Web.Areas.Admin.Models.Categorize
{
    /// <summary>
    /// Represents a product search model
    /// </summary>
    public partial class QuestionSearchModel : BaseSearchModel
    {
        #region Ctor

        public QuestionSearchModel()
        {
            AvailableCategories = new List<SelectListItem>();
            AvailableQuestionTypes = new List<SelectListItem>();
            AvailablePublishedOptions = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        [DisplayName("Product name")]
        public string SearchQuestionName { get; set; }

        [DisplayName("Category")]
        public int SearchCategoryId { get; set; }

        [DisplayName("Search subcategories")]
        public bool SearchIncludeSubCategories { get; set; }

        [DisplayName("Question type")]
        public int SearchQuestionTypeId { get; set; }

        [DisplayName("Published")]
        public int SearchPublishedId { get; set; }

        public IList<SelectListItem> AvailableCategories { get; set; }

        public IList<SelectListItem> AvailableQuestionTypes { get; set; }

        public IList<SelectListItem> AvailablePublishedOptions { get; set; }

        #endregion
    }

}
