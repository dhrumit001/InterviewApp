using App.Core.Domain.Categorize;
using App.Web.Areas.Admin.Models.Categorize;

namespace App.Web.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the category model factory
    /// </summary>
    public partial interface ICategoryModelFactory
    {
        /// <summary>
        /// Prepare category search model
        /// </summary>
        /// <param name="searchModel">Category search model</param>
        /// <returns>Category search model</returns>
        CategorySearchModel PrepareCategorySearchModel(CategorySearchModel searchModel);

        /// <summary>
        /// Prepare paged category list model
        /// </summary>
        /// <param name="searchModel">Category search model</param>
        /// <returns>Category list model</returns>
        CategoryListModel PrepareCategoryListModel(CategorySearchModel searchModel);

        /// <summary>
        /// Prepare category model
        /// </summary>
        /// <param name="model">Category model</param>
        /// <param name="category">Category</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>Category model</returns>
        CategoryModel PrepareCategoryModel(CategoryModel model, Category category, bool excludeProperties = false);


        /// <summary>
        /// Prepare paged category product list model
        /// </summary>
        /// <param name="searchModel">Category product search model</param>
        /// <param name="category">Category</param>
        /// <returns>Category product list model</returns>
        CategoryQuestionListModel PrepareCategoryQuestionListModel(CategoryQuestionSearchModel searchModel, Category category);

        /// <summary>
        /// Prepare product search model to add to the category
        /// </summary>
        /// <param name="searchModel">Product search model to add to the category</param>
        /// <returns>Product search model to add to the category</returns>
        AddQuestionToCategorySearchModel PrepareAddQuestionToCategorySearchModel(AddQuestionToCategorySearchModel searchModel);

        /// <summary>
        /// Prepare paged product list model to add to the category
        /// </summary>
        /// <param name="searchModel">Product search model to add to the category</param>
        /// <returns>Product list model to add to the category</returns>
        AddQuestionToCategoryListModel PrepareAddQuestionToCategoryListModel(AddQuestionToCategorySearchModel searchModel);
        
    }
}
