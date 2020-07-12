using App.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace App.Web.Areas.Admin.Models.Categorize
{
    /// <summary>
    /// Represents a category question model
    /// </summary>
    public partial class CategoryQuestionModel : BaseEntityModel
    {
        #region Properties

        public int CategoryId { get; set; }

        public int QuestionId { get; set; }

        [DisplayName("Question")]
        public string QuestionName { get; set; }

        [DisplayName("DisplayOrder")]
        public int DisplayOrder { get; set; }

        #endregion
    }
}
