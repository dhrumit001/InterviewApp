using App.Core;
using App.Core.Domain.Categorize;
using System.Collections.Generic;

namespace App.Services.Categorize
{
    /// <summary>
    /// Question service
    /// </summary>
    public interface IQuestionService
    {
        #region Questions

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="question">Question</param>
        void DeleteQuestion(Question question);

        /// <summary>
        /// Delete products
        /// </summary>
        /// <param name="questions">Questions</param>
        void DeleteQuestions(IList<Question> questions);

        /// <summary>
        /// Gets question
        /// </summary>
        /// <param name="questionId">Question identifier</param>
        /// <returns>Question</returns>
        Question GetQuestionById(int questionId);

        /// <summary>
        /// Gets questions by identifier
        /// </summary>
        /// <param name="questionIds">Question identifiers</param>
        /// <returns>Questions</returns>
        IList<Question> GetQuestionsByIds(int[] questionIds);

        /// <summary>
        /// Inserts a question
        /// </summary>
        /// <param name="question">Question</param>
        void InsertQuestion(Question question);

        /// <summary>
        /// Updates the question
        /// </summary>
        /// <param name="question">Question</param>
        void UpdateQuestion(Question question);

        /// <summary>
        /// Updates the products
        /// </summary>
        /// <param name="products">Product</param>
        void UpdateQuestions(IList<Question> questions);

        /// <summary>
        /// Get number of product (published and visible) in certain category
        /// </summary>
        /// <param name="categoryIds">Category identifiers</param>
        /// <param name="storeId">Store identifier; 0 to load all records</param>
        /// <returns>Number of products</returns>
        int GetNumberOfQuestionsInCategory(IList<int> categoryIds = null);

        /// <summary>
        /// Search products
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="categoryIds">Category identifiers</param>
        /// <param name="manufacturerId">Manufacturer identifier; 0 to load all records</param>
        /// <param name="storeId">Store identifier; 0 to load all records</param>
        /// <param name="vendorId">Vendor identifier; 0 to load all records</param>
        /// <param name="warehouseId">Warehouse identifier; 0 to load all records</param>
        /// <param name="productType">Product type; 0 to load all records</param>
        /// <param name="visibleIndividuallyOnly">A values indicating whether to load only products marked as "visible individually"; "false" to load all records; "true" to load "visible individually" only</param>
        /// <param name="markedAsNewOnly">A values indicating whether to load only products marked as "new"; "false" to load all records; "true" to load "marked as new" only</param>
        /// <param name="featuredProducts">A value indicating whether loaded products are marked as featured (relates only to categories and manufacturers). 0 to load featured products only, 1 to load not featured products only, null to load all products</param>
        /// <param name="priceMin">Minimum price; null to load all records</param>
        /// <param name="priceMax">Maximum price; null to load all records</param>
        /// <param name="productTagId">Product tag identifier; 0 to load all records</param>
        /// <param name="keywords">Keywords</param>
        /// <param name="searchDescriptions">A value indicating whether to search by a specified "keyword" in product descriptions</param>
        /// <param name="searchManufacturerPartNumber">A value indicating whether to search by a specified "keyword" in manufacturer part number</param>
        /// <param name="searchSku">A value indicating whether to search by a specified "keyword" in product SKU</param>
        /// <param name="searchProductTags">A value indicating whether to search by a specified "keyword" in product tags</param>
        /// <param name="filteredSpecs">Filtered product specification identifiers</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="overridePublished">
        /// null - process "Published" property according to "showHidden" parameter
        /// true - load only "Published" products
        /// false - load only "Unpublished" products
        /// </param>
        /// <returns>Products</returns>
        IPagedList<Question> GetAllQuestions(
            string questionName,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            IList<int> categoryIds = null,
            QuestionType? questionType = null,
            bool? overridePublished = null);

        #endregion

        #region Question options

        /// <summary>
        /// Inserts a question option
        /// </summary>
        /// <param name="questionOption">Question option</param>
        void InsertQuestionOption(QuestionOption questionOption);
        
        /// <summary>
        /// Updates a question option
        /// </summary>
        /// <param name="productPicture">Question option</param>
        void UpdateQuestionOption(QuestionOption questionOption);
        
        #endregion
    }
}
