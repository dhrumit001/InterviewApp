using App.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

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

        [DisplayName("Admin.Catalog.Categories.Products.Fields.Product")]
        public string QuestionName { get; set; }

        [DisplayName("Admin.Catalog.Categories.Products.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }

        #endregion
    }
}
