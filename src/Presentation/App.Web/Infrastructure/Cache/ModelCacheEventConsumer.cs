using App.Core.Caching;
using App.Core.Domain.Categorize;
using App.Core.Events;
using App.Services.Events;
using App.Web.Areas.Admin.Infrastructure.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Infrastructure.Cache
{
    /// <summary>
    /// Model cache event consumer (used for caching of presentation layer models)
    /// </summary>
    //public partial class ModelCacheEventConsumer : //categories
    //    IConsumer<EntityInsertedEvent<Category>>,
    //    IConsumer<EntityUpdatedEvent<Category>>,
    //    IConsumer<EntityDeletedEvent<Category>>,
    //    //product categories
    //    IConsumer<EntityInsertedEvent<QuestionCategory>>,
    //    IConsumer<EntityUpdatedEvent<QuestionCategory>>,
    //    IConsumer<EntityDeletedEvent<QuestionCategory>>,
    //    //products
    //    IConsumer<EntityInsertedEvent<Question>>,
    //    IConsumer<EntityUpdatedEvent<Question>>,
    //    IConsumer<EntityDeletedEvent<Question>>
    //{
    //    #region Fields

    //    private readonly IStaticCacheManager _cacheManager;

    //    #endregion

    //    #region Ctor

    //    public ModelCacheEventConsumer(IStaticCacheManager cacheManager)
    //    {
    //        _cacheManager = cacheManager;
    //    }

    //    #endregion

    //    #region Methods

    //    //categories
    //    public void HandleEvent(EntityInsertedEvent<Category> eventMessage)
    //    {
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.SearchCategoriesPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.CategoryAllPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.CategoryXmlAllPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.CategorySubcategoriesPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.CategoryHomepagePrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.SitemapPrefixCacheKey);
    //    }
    //    public void HandleEvent(EntityUpdatedEvent<Category> eventMessage)
    //    {
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.SearchCategoriesPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.ProductBreadcrumbPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.CategoryAllPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.CategoryXmlAllPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.CategoryBreadcrumbPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.CategorySubcategoriesPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.CategoryHomepagePrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.SitemapPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(string.Format(AppModelCacheDefaults.CategoryPicturePrefixCacheKeyById, eventMessage.Entity.Id));
    //    }
    //    public void HandleEvent(EntityDeletedEvent<Category> eventMessage)
    //    {
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.SearchCategoriesPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.ProductBreadcrumbPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.CategoryAllPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.CategoryXmlAllPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.CategoryBreadcrumbPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.CategorySubcategoriesPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.CategoryHomepagePrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.SitemapPrefixCacheKey);
    //    }

    //    //product categories
    //    public void HandleEvent(EntityInsertedEvent<ProductCategory> eventMessage)
    //    {
    //        _cacheManager.RemoveByPrefix(string.Format(AppModelCacheDefaults.ProductBreadcrumbPrefixCacheKeyById, eventMessage.Entity.ProductId));
    //        if (_catalogSettings.ShowCategoryProductNumber)
    //        {
    //            //depends on CatalogSettings.ShowCategoryProductNumber (when enabled)
    //            //so there's no need to clear this cache in other cases
    //            _cacheManager.RemoveByPrefix(AppModelCacheDefaults.CategoryAllPrefixCacheKey);
    //            _cacheManager.RemoveByPrefix(AppModelCacheDefaults.CategoryXmlAllPrefixCacheKey);
    //        }
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.CategoryNumberOfProductsPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(string.Format(AppModelCacheDefaults.CategoryHasFeaturedProductsPrefixCacheKeyById, eventMessage.Entity.CategoryId));
    //    }
    //    public void HandleEvent(EntityUpdatedEvent<ProductCategory> eventMessage)
    //    {
    //        _cacheManager.RemoveByPrefix(string.Format(AppModelCacheDefaults.ProductBreadcrumbPrefixCacheKeyById, eventMessage.Entity.ProductId));
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.CategoryNumberOfProductsPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(string.Format(AppModelCacheDefaults.CategoryHasFeaturedProductsPrefixCacheKeyById, eventMessage.Entity.CategoryId));
    //    }
    //    public void HandleEvent(EntityDeletedEvent<ProductCategory> eventMessage)
    //    {
    //        _cacheManager.RemoveByPrefix(string.Format(AppModelCacheDefaults.ProductBreadcrumbPrefixCacheKeyById, eventMessage.Entity.ProductId));
    //        if (_catalogSettings.ShowCategoryProductNumber)
    //        {
    //            //depends on CatalogSettings.ShowCategoryProductNumber (when enabled)
    //            //so there's no need to clear this cache in other cases
    //            _cacheManager.RemoveByPrefix(AppModelCacheDefaults.CategoryAllPrefixCacheKey);
    //            _cacheManager.RemoveByPrefix(AppModelCacheDefaults.CategoryXmlAllPrefixCacheKey);
    //        }
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.CategoryNumberOfProductsPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(string.Format(AppModelCacheDefaults.CategoryHasFeaturedProductsPrefixCacheKeyById, eventMessage.Entity.CategoryId));
    //    }

    //    //products
    //    public void HandleEvent(EntityInsertedEvent<Product> eventMessage)
    //    {
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.SitemapPrefixCacheKey);
    //    }
    //    public void HandleEvent(EntityUpdatedEvent<Product> eventMessage)
    //    {
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.HomepageBestsellersIdsPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.ProductsAlsoPurchasedIdsPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.ProductsRelatedIdsPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.SitemapPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(string.Format(AppModelCacheDefaults.ProductReviewsPrefixCacheKeyById, eventMessage.Entity.Id));
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.ProductTagByProductPrefixCacheKey);
    //    }
    //    public void HandleEvent(EntityDeletedEvent<Product> eventMessage)
    //    {
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.HomepageBestsellersIdsPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.ProductsAlsoPurchasedIdsPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.ProductsRelatedIdsPrefixCacheKey);
    //        _cacheManager.RemoveByPrefix(AppModelCacheDefaults.SitemapPrefixCacheKey);
    //    }

    //    #endregion
    //}
}
