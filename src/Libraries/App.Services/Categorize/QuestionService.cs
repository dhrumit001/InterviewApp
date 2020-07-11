using App.Core;
using App.Core.Caching;
using App.Core.Data;
using App.Core.Domain.Categorize;
using App.Data;
using App.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Services.Categorize
{
    /// <summary>
    /// Question service
    /// </summary>
    public class QuestionService : IQuestionService
    {
        #region Fields

        private readonly ICacheManager _cacheManager;
        private readonly IDbContext _dbContext;
        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<QuestionOption> _questionOptionRepository;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public QuestionService(
            ICacheManager cacheManager,
            IDbContext dbContext,
            IEventPublisher eventPublisher,
            IRepository<Question> questionRepository,
            IRepository<QuestionOption> questionOptionRepository,
            IWorkContext workContext
            )
        {
            _cacheManager = cacheManager;
            _dbContext = dbContext;
            _eventPublisher = eventPublisher;
            _questionRepository = questionRepository;
            _questionOptionRepository = questionOptionRepository;
            _workContext = workContext;

        }

        #endregion

        #region Methods

        #region Questions

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="product">Product</param>
        public virtual void DeleteQuestion(Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            question.Deleted = true;
            //delete product
            UpdateQuestion(question);

            //event notification
            _eventPublisher.EntityDeleted(question);
        }

        /// <summary>
        /// Delete products
        /// </summary>
        /// <param name="products">Products</param>
        public virtual void DeleteQuestions(IList<Question> questions)
        {
            if (questions == null)
                throw new ArgumentNullException(nameof(questions));

            foreach (var question in questions)
            {
                question.Deleted = true;
            }

            //delete product
            UpdateQuestions(questions);

            foreach (var question in questions)
            {
                //event notification
                _eventPublisher.EntityDeleted(question);
            }
        }

        /// <summary>
        /// Gets product
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <returns>Product</returns>
        public virtual Question GetQuestionById(int questionId)
        {
            if (questionId == 0)
                return null;

            var key = string.Format(AppCatalogDefaults.QuestionsByIdCacheKey, questionId);
            return _cacheManager.Get(key, () => _questionRepository.GetById(questionId));
        }

        /// <summary>
        /// Get products by identifiers
        /// </summary>
        /// <param name="productIds">Product identifiers</param>
        /// <returns>Products</returns>
        public virtual IList<Question> GetQuestionsByIds(int[] questionIds)
        {
            if (questionIds == null || questionIds.Length == 0)
                return new List<Question>();

            var query = from p in _questionRepository.Table
                        where questionIds.Contains(p.Id) && !p.Deleted
                        select p;
            var questions = query.ToList();
            //sort by passed identifiers
            var sortedQuestions = new List<Question>();
            foreach (var id in questionIds)
            {
                var question = questions.Find(x => x.Id == id);
                if (question != null)
                    sortedQuestions.Add(question);
            }

            return sortedQuestions;
        }

        /// <summary>
        /// Inserts a product
        /// </summary>
        /// <param name="question">Product</param>
        public virtual void InsertQuestion(Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            //insert
            _questionRepository.Insert(question);

            //clear cache
            _cacheManager.RemoveByPrefix(AppCatalogDefaults.QuestionsPrefixCacheKey);

            //event notification
            _eventPublisher.EntityInserted(question);
        }

        /// <summary>
        /// Updates the product
        /// </summary>
        /// <param name="question">Product</param>
        public virtual void UpdateQuestion(Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            //update
            _questionRepository.Update(question);

            //cache
            _cacheManager.RemoveByPrefix(AppCatalogDefaults.QuestionsPrefixCacheKey);

            //event notification
            _eventPublisher.EntityUpdated(question);
        }

        /// <summary>
        /// Update products
        /// </summary>
        /// <param name="questions">Products</param>
        public virtual void UpdateQuestions(IList<Question> questions)
        {
            if (questions == null)
                throw new ArgumentNullException(nameof(questions));

            //update
            _questionRepository.Update(questions);

            //cache
            _cacheManager.RemoveByPrefix(AppCatalogDefaults.QuestionsPrefixCacheKey);

            //event notification
            foreach (var question in questions)
            {
                _eventPublisher.EntityUpdated(question);
            }
        }

        /// <summary>
        /// Get number of product (published and visible) in certain category
        /// </summary>
        /// <param name="categoryIds">Category identifiers</param>
        /// <param name="storeId">Store identifier; 0 to load all records</param>
        /// <returns>Number of products</returns>
        public virtual int GetNumberOfQuestionsInCategory(IList<int> categoryIds = null)
        {
            //validate "categoryIds" parameter
            if (categoryIds != null && categoryIds.Contains(0))
                categoryIds.Remove(0);

            var query = _questionRepository.Table;
            query = query.Where(p => !p.Deleted && p.Published);

            //category filtering
            if (categoryIds != null && categoryIds.Any())
            {
                query = from p in query
                        from pc in p.QuestionCategories.Where(pc => categoryIds.Contains(pc.CategoryId))
                        select p;
            }

            //only distinct products
            var result = query.Select(p => p.Id).Distinct().Count();
            return result;
        }

        /// <summary>
        /// Search products
        /// </summary>
        /// <param name="filterableSpecificationAttributeOptionIds">The specification attribute option identifiers applied to loaded products (all pages)</param>
        /// <param name="loadFilterableSpecificationAttributeOptionIds">A value indicating whether we should load the specification attribute option identifiers applied to loaded products (all pages)</param>
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
        /// <param name="languageId">Language identifier (search for text searching)</param>
        /// <param name="filteredSpecs">Filtered product specification identifiers</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="overridePublished">
        /// null - process "Published" property according to "showHidden" parameter
        /// true - load only "Published" products
        /// false - load only "Unpublished" products
        /// </param>
        /// <returns>Products</returns>
        public virtual IPagedList<Question> GetAllQuestions(
            string questionName = null,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            IList<int> categoryIds = null,
            QuestionType? questionType = null,
            bool? overridePublished = null
            )
        {

            var query = _questionRepository.Table;
            if (overridePublished != null)
                query = query.Where(c => c.Published == overridePublished);
            if (!string.IsNullOrWhiteSpace(questionName))
                query = query.Where(c => c.Name.Contains(questionName));

            if (categoryIds?.Count > 0)
            {
                //validate "categoryIds" parameter
                if (categoryIds.Contains(0))
                    categoryIds.Remove(0);

                if (categoryIds.Any())
                    query = query.Where(c => c.QuestionCategories.Any(qc => categoryIds.Any(cd => cd == qc.CategoryId)));
            }

            if (questionType != null)
            {
                var questionTypeId = Convert.ToInt32(questionType);
                query = query.Where(c => c.QuestionTypeId == questionTypeId);
            }

            query = query.Where(c => !c.Deleted);
            query = query.OrderBy(c => c.Id);

            var unsortedQuestions = query.ToList();

            //paging
            return new PagedList<Question>(unsortedQuestions, pageIndex, pageSize);
        }

        #endregion

        #region Question options

        /// <summary>
        /// Inserts a question option
        /// </summary>
        /// <param name="questionOption">Question option</param>
        public virtual void InsertQuestionOption(QuestionOption questionOption)
        {
            if (questionOption == null)
                throw new ArgumentNullException(nameof(questionOption));

            _questionOptionRepository.Insert(questionOption);

            //event notification
            _eventPublisher.EntityInserted(questionOption);
        }

        /// <summary>
        /// Updates a question option
        /// </summary>
        /// <param name="productPicture">Question option</param>
        public virtual void UpdateQuestionOption(QuestionOption questionOption)
        {
            if (questionOption == null)
                throw new ArgumentNullException(nameof(questionOption));

            _questionOptionRepository.Update(questionOption);

            //event notification
            _eventPublisher.EntityUpdated(questionOption);
        }

        /// <summary>
        /// Gets all question options
        /// </summary>
        /// <param name="questionOption">Question option</param>
        public virtual IList<QuestionOption> GetAllQuestionOptions(int questionId)
        {
            return _questionOptionRepository.Table.
                Where(qo => qo.QuestionId == questionId).ToList();
        }

        #endregion

        #endregion
    }
}
