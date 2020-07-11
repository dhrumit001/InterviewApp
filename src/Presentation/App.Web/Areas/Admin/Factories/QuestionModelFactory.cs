using App.Core;
using App.Core.Domain.Categorize;
using App.Services.Categorize;
using App.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using App.Web.Areas.Admin.Models.Categorize;
using App.Web.Framework.Models.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Web.Areas.Admin.Factories
{
    public class QuestionModelFactory : IQuestionModelFactory
    {
        #region Fields

        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        private readonly ICategoryService _categoryService;
        private readonly IQuestionService _questionService;
        private readonly ISettingModelFactory _settingModelFactory;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public QuestionModelFactory(IBaseAdminModelFactory baseAdminModelFactory,
            ICategoryService categoryService,
            IQuestionService questionService,
            ISettingModelFactory settingModelFactory,
            IWorkContext workContext)
        {
            _baseAdminModelFactory = baseAdminModelFactory;
            _categoryService = categoryService;
            _questionService = questionService;
            _settingModelFactory = settingModelFactory;
            _workContext = workContext;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepare product search model
        /// </summary>
        /// <param name="searchModel">Product search model</param>
        /// <returns>Product search model</returns>
        public virtual QuestionSearchModel PrepareQuestionSearchModel(QuestionSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //prepare available categories
            _baseAdminModelFactory.PrepareCategories(searchModel.AvailableCategories);

            //prepare available product types
            _baseAdminModelFactory.PrepareQuestionTypes(searchModel.AvailableQuestionTypes);

            //prepare "published" filter (0 - all; 1 - published only; 2 - unpublished only)
            searchModel.AvailablePublishedOptions.Add(new SelectListItem
            {
                Value = "0",
                Text = "All"
            });
            searchModel.AvailablePublishedOptions.Add(new SelectListItem
            {
                Value = "1",
                Text = "Published only"
            });
            searchModel.AvailablePublishedOptions.Add(new SelectListItem
            {
                Value = "2",
                Text = "Unpublished only"
            });

            //prepare grid
            searchModel.SetGridPageSize();

            return searchModel;
        }

        /// <summary>
        /// Prepare paged product list model
        /// </summary>
        /// <param name="searchModel">Product search model</param>
        /// <returns>Product list model</returns>
        public virtual QuestionListModel PrepareQuestionListModel(QuestionSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get parameters to filter comments
            var overridePublished = searchModel.SearchPublishedId == 0 ? null : (bool?)(searchModel.SearchPublishedId == 1);
            var categoryIds = new List<int> { searchModel.SearchCategoryId };
            if (searchModel.SearchIncludeSubCategories && searchModel.SearchCategoryId > 0)
            {
                var childCategoryIds = _categoryService.GetChildCategoryIds(parentCategoryId: searchModel.SearchCategoryId, showHidden: true);
                categoryIds.AddRange(childCategoryIds);
            }

            //get products
            var questions = _questionService.GetAllQuestions(searchModel.SearchQuestionName,
                categoryIds: categoryIds,
                questionType: searchModel.SearchQuestionTypeId > 0 ? (QuestionType?)searchModel.SearchQuestionTypeId : null,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize,
                overridePublished: overridePublished);

            //prepare list model
            var model = new QuestionListModel().PrepareToGrid(searchModel, questions, () =>
            {
                return questions.Select(product =>
                {
                    //fill in model values from the entity
                    var questionModel = product.ToModel<QuestionModel>();
                    questionModel.QuestionTypeName = product.QuestionType.ToString();
                    return questionModel;
                });
            });

            return model;
        }

        /// <summary>
        /// Prepare product model
        /// </summary>
        /// <param name="model">Product model</param>
        /// <param name="question">Product</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>Product model</returns>
        public virtual QuestionModel PrepareQuestionModel(QuestionModel model, Question question, bool excludeProperties = false)
        {
            if (question != null)
            {
                //fill in model values from the entity
                if (model == null)
                {
                    model = question.ToModel<QuestionModel>();
                    model.AnswerDescription = question.QuestionAnswer?.Description;
                }

                if (!excludeProperties)
                {
                    model.SelectedCategoryIds = _categoryService.GetQuestionCategoriesByQuestionId(question.Id, true)
                        .Select(productCategory => productCategory.CategoryId).ToList();
                }

            }

            //set default values for the new model
            if (question == null)
            {
                model.Published = true;
            }

            //prepare model categories
            _baseAdminModelFactory.PrepareCategories(model.AvailableCategories, false);
            foreach (var categoryItem in model.AvailableCategories)
            {
                categoryItem.Selected = int.TryParse(categoryItem.Value, out var categoryId)
                    && model.SelectedCategoryIds.Contains(categoryId);
            }

            return model;
        }

        #endregion
    }
}
