using App.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;

namespace App.Web.Areas.Admin.Models.Categorize
{
    /// <summary>
    /// Represents a question model
    /// </summary>
    public partial class QuestionModel : BaseEntityModel
    {
        #region Ctor

        public QuestionModel()
        {
            SelectedCategoryIds = new List<int>();
            AvailableCategories = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        [DisplayName("Question type")]
        public int QuestionTypeId { get; set; }

        [DisplayName("Question type")]
        public string QuestionTypeName { get; set; }

        [DisplayName("Question name")]
        public string Name { get; set; }

        [DisplayName("Answer description")]
        public string AnswerDescription { get; set; }

        [DisplayName("Published")]
        public bool Published { get; set; }

        //categories
        [DisplayName("Categories")]
        public IList<int> SelectedCategoryIds { get; set; }
        public IList<SelectListItem> AvailableCategories { get; set; }

        #endregion

        public int MyProperty { get; set; }
    }
}
