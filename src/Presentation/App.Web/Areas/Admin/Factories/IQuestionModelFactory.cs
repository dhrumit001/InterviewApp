using App.Core.Domain.Categorize;
using App.Web.Areas.Admin.Models.Categorize;


namespace App.Web.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the product model factory
    /// </summary>
    public interface IQuestionModelFactory
    {
        /// <summary>
        /// Prepare product search model
        /// </summary>
        /// <param name="searchModel">Product search model</param>
        /// <returns>Product search model</returns>
        QuestionSearchModel PrepareQuestionSearchModel(QuestionSearchModel searchModel);

        /// <summary>
        /// Prepare paged product list model
        /// </summary>
        /// <param name="searchModel">Product search model</param>
        /// <returns>Product list model</returns>
        QuestionListModel PrepareQuestionListModel(QuestionSearchModel searchModel);

        /// <summary>
        /// Prepare product model
        /// </summary>
        /// <param name="model">Product model</param>
        /// <param name="product">Product</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>Product model</returns>
        QuestionModel PrepareQuestionModel(QuestionModel model, Question question, bool excludeProperties = false);
    }
}
