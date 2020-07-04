
namespace App.Services.Categorize
{
    /// <summary>
    /// Represents default values related to catalog services
    /// </summary>
    public static partial class AppCatalogDefaults
    {
        #region Categories

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// {1} : product ID
        /// {2} : current customer ID
        /// {3} : store ID
        /// </remarks>
        public static string QuestionCategoriesAllByQuestionIdCacheKey => "App.questioncategory.allbyquestionid-{0}-{1}";

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// {1} : category ID
        /// {2} : page index
        /// {3} : page size
        /// {4} : current customer ID
        /// {5} : store ID
        /// </remarks>
        public static string QuestionCategoriesAllByCategoryIdCacheKey => "App.questioncategory.allbycategoryid-{0}-{1}-{2}-{3}";

        /// <summary>
        /// Gets a key pattern to clear cache
        /// </summary>
        public static string QuestionCategoriesPrefixCacheKey => "App.questioncategory.";

        #endregion

        #region Products

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : product ID
        /// </remarks>
        public static string QuestionsByIdCacheKey => "App.question.id-{0}";

        /// <summary>
        /// Gets a key pattern to clear cache
        /// </summary>
        public static string QuestionsPrefixCacheKey => "App.question.";

        #endregion
    }
}
