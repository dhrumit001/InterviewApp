using App.Core;
using App.Core.Domain.Categorize;
using App.Services.Categorize;
using App.Services.ExportImport.Help;
using App.Services.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Services.ExportImport
{
    /// <summary>
    /// Export manager
    /// </summary>
    public partial class ExportManager : IExportManager
    {
        #region Fields

        private readonly ICategoryService _categoryService;
        private readonly IQuestionService _questionService;
        private readonly IPictureService _pictureService;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public ExportManager(ICategoryService categoryService,
            IPictureService pictureService,
            IWorkContext workContext)
        {
            _categoryService = categoryService;
            _pictureService = pictureService;
            _workContext = workContext;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Returns the path to the image file by ID
        /// </summary>
        /// <param name="pictureId">Picture ID</param>
        /// <returns>Path to the image file</returns>
        protected virtual string GetPictures(int pictureId)
        {
            var picture = _pictureService.GetPictureById(pictureId);
            return _pictureService.GetThumbLocalPath(picture);
        }

        /// <summary>
        /// Returns the list of categories for a product separated by a ";"
        /// </summary>
        /// <param name="product">Question</param>
        /// <returns>List of categories</returns>
        protected virtual string GetCategories(Question question)
        {
            string categoryNames = null;
            foreach (var pc in _categoryService.GetQuestionCategoriesByQuestionId(question.Id, true))
            {
                categoryNames += pc.Category.Id.ToString();

                categoryNames += ";";
            }

            return categoryNames;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Export questions to XLSX
        /// </summary>
        /// <param name="questions">Questions</param>
        public virtual byte[] ExportQuestionsToXlsx(IEnumerable<Question> questions)
        {
            var properties = new[]
            {
                new PropertyByName<Question>("QuestionId", p => p.Id),
                new PropertyByName<Question>("QuestionType", p => p.QuestionTypeId)
                {
                    DropDownElements = QuestionType.SimpleQuestion.ToSelectList(useLocalization: false)
                },
                new PropertyByName<Question>("Name", p => p.Name),
                new PropertyByName<Question>("Published", p => p.Published),
                new PropertyByName<Question>("Categories", GetCategories),
            };

            var productList = questions.ToList();

            return new PropertyManager<Question>(properties).ExportToXlsx(productList);
        }

        #endregion
    }
}

