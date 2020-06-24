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


        #endregion

        #region Ctor

        public CategoryModelFactory(IBaseAdminModelFactory baseAdminModelFactory,
            ICategoryService categoryService)
        {
            _baseAdminModelFactory = baseAdminModelFactory;
            _categoryService = categoryService;
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

            }

            //prepare available parent categories
            _baseAdminModelFactory.PrepareCategories(model.AvailableCategories,
                defaultItemText: "[None]");

            return model;
        }

        #endregion
    }
}
