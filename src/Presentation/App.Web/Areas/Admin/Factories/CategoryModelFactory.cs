using App.Core.Domain.Categorize;
using App.Services.Categorize;
using App.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using App.Web.Areas.Admin.Models.Categorize;
using App.Web.Framework.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the category model factory implementation
    /// </summary>
    public partial class CategoryModelFactory : ICategoryModelFactory
    {
        #region Fields

        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        private readonly ICategoryService _categoryService;
        private readonly IQuestionService _questionService;

        #endregion

        #region Ctor

        public CategoryModelFactory(IBaseAdminModelFactory baseAdminModelFactory,
            ICategoryService categoryService,
            IQuestionService questionService)
        {
            _baseAdminModelFactory = baseAdminModelFactory;
            _categoryService = categoryService;
            _questionService = questionService;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Prepare category product search model
        /// </summary>
        /// <param name="searchModel">Category product search model</param>
        /// <param name="category">Category</param>
        /// <returns>Category product search model</returns>
        protected virtual CategoryQuestionSearchModel PrepareCategoryQuestionSearchModel(CategoryQuestionSearchModel searchModel, Category category)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            if (category == null)
                throw new ArgumentNullException(nameof(category));

            searchModel.CategoryId = category.Id;

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepare category search model
        /// </summary>
        /// <param name="searchModel">Category search model</param>
        /// <returns>Category search model</returns>
        public virtual CategorySearchModel PrepareCategorySearchModel(CategorySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
        }

        /// <summary>
        /// Prepare paged category list model
        /// </summary>
        /// <param name="searchModel">Category search model</param>
        /// <returns>Category list model</returns>
        public virtual CategoryListModel PrepareCategoryListModel(CategorySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get categories
            var categories = _categoryService.GetAllCategories(categoryName: searchModel.SearchCategoryName,
                showHidden: true,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare grid model
            var model = new CategoryListModel().PrepareToGrid(searchModel, categories, () =>
            {
                return categories.Select(category =>
                {
                    //fill in model values from the entity
                    var categoryModel = category.ToModel<CategoryModel>();

                    //fill in additional values (not existing in the entity)
                    categoryModel.Breadcrumb = _categoryService.GetFormattedBreadCrumb(category);

                    return categoryModel;
                });
            });

            return model;
        }

        /// <summary>
        /// Prepare category model
        /// </summary>
        /// <param name="model">Category model</param>
        /// <param name="category">Category</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>Category model</returns>
        public virtual CategoryModel PrepareCategoryModel(CategoryModel model, Category category, bool excludeProperties = false)
        {
            if (category != null)
            {
                //fill in model values from the entity
                if (model == null)
                {
                    model = category.ToModel<CategoryModel>();
                }

                //prepare nested search model
                PrepareCategoryQuestionSearchModel(model.CategoryQuestionSearchModel, category);
            }

            //prepare available parent categories
            _baseAdminModelFactory.PrepareCategories(model.AvailableCategories,
                defaultItemText: "[None]");

            return model;
        }


        /// <summary>
        /// Prepare paged category product list model
        /// </summary>
        /// <param name="searchModel">Category product search model</param>
        /// <param name="category">Category</param>
        /// <returns>Category product list model</returns>
        public virtual CategoryQuestionListModel PrepareCategoryQuestionListModel(CategoryQuestionSearchModel searchModel, Category category)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            if (category == null)
                throw new ArgumentNullException(nameof(category));

            //get question categories
            var questionCategories = _categoryService.GetQuestionCategoriesByCategoryId(category.Id,
                showHidden: true,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare grid model
            var model = new CategoryQuestionListModel().PrepareToGrid(searchModel, questionCategories, () =>
            {
                return questionCategories.Select(questionCategory =>
                {
                    //fill in model values from the entity
                    var categoryQuestionModel = questionCategory.ToModel<CategoryQuestionModel>();

                    //fill in additional values (not existing in the entity)
                    categoryQuestionModel.QuestionName = _questionService.GetQuestionById(questionCategory.QuestionId)?.Name;

                    return categoryQuestionModel;
                });
            });

            return model;
        }

        /// <summary>
        /// Prepare product search model to add to the category
        /// </summary>
        /// <param name="searchModel">Product search model to add to the category</param>
        /// <returns>Product search model to add to the category</returns>
        public virtual AddQuestionToCategorySearchModel PrepareAddQuestionToCategorySearchModel(AddQuestionToCategorySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //prepare available categories
            _baseAdminModelFactory.PrepareCategories(searchModel.AvailableCategories);

            //prepare available product types
            _baseAdminModelFactory.PrepareQuestionTypes(searchModel.AvailableQuestionTypes);

            //prepare page parameters
            searchModel.SetPopupGridPageSize();

            return searchModel;
        }

        /// <summary>
        /// Prepare paged product list model to add to the category
        /// </summary>
        /// <param name="searchModel">Product search model to add to the category</param>
        /// <returns>Product list model to add to the category</returns>
        public virtual AddQuestionToCategoryListModel PrepareAddQuestionToCategoryListModel(AddQuestionToCategorySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get products
            var products = _questionService.GetAllQuestions(categoryIds: new List<int> { searchModel.SearchCategoryId },
                questionType: searchModel.SearchQuestionTypeId > 0 ? (QuestionType?)searchModel.SearchQuestionTypeId : null,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare grid model
            var model = new AddQuestionToCategoryListModel().PrepareToGrid(searchModel, products, () =>
            {
                return products.Select(product =>
                {
                    var productModel = product.ToModel<QuestionModel>();
                    return productModel;
                });
            });

            return model;
        }

        #endregion
    }
}
