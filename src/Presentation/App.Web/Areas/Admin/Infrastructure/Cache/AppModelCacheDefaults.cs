
namespace App.Web.Areas.Admin.Infrastructure.Cache
{
    public static partial class AppModelCacheDefaults
    {
        
        /// <summary>
        /// Key for categories caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// </remarks>
        public static string CategoriesListKey => "App.pres.admin.categories.list-{0}";
        public static string CategoriesListPrefixCacheKey => "App.pres.admin.categories.list";

    }
}
