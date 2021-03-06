﻿using App.Web.Framework.Models;
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

            CategoryQuestionSearchModel = new CategoryQuestionSearchModel();
        }

        #endregion

        #region Properties

        [DisplayName("Picture")]
        public string PictureThumbnailUrl { get; set; }

        [DisplayName("Category name")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Question category")]
        public int ParentCategoryId { get; set; }

        [UIHint("Picture")]
        [DisplayName("Picture")]
        public int PictureId { get; set; }

        [DisplayName("Display order")]
        public int DisplayOrder { get; set; }

        [DisplayName("Published")]
        public bool Published { get; set; }

        [DisplayName("Deleted")]
        public bool Deleted { get; set; }

        public string Breadcrumb { get; set; }

        public int TotalQuestions { get; set; }
        public int PublishedQuestions { get; set; }

        public IList<SelectListItem> AvailableCategories { get; set; }

        public CategoryQuestionSearchModel CategoryQuestionSearchModel { get; set; }

        #endregion
    }

}
