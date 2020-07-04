using App.Core.Caching;
using App.Core.Domain.Categorize;
using App.Core.Domain.Logging;
using App.Services;
using App.Services.Categorize;
using App.Web.Areas.Admin.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace App.Web.Areas.Admin.Factories
{
    public class BaseAdminModelFactory : IBaseAdminModelFactory
    {

        #region Fields

        private readonly ICategoryService _categoryService;
        private readonly IStaticCacheManager _cacheManager;

        #endregion

        #region Ctor

        public BaseAdminModelFactory(ICategoryService categoryService,
            IStaticCacheManager cacheManager)
        {
            _categoryService = categoryService;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Prepare default item
        /// </summary>
        /// <param name="items">Available items</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use "All" text</param>
        protected virtual void PrepareDefaultItem(IList<SelectListItem> items, bool withSpecialDefaultItem, string defaultItemText = null)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            //whether to insert the first special item for the default value
            if (!withSpecialDefaultItem)
                return;

            //at now we use "0" as the default value
            const string value = "0";

            //prepare item text
            defaultItemText = defaultItemText ?? "All";

            //insert this default item at first
            items.Insert(0, new SelectListItem { Text = defaultItemText, Value = value });
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepare available categories
        /// </summary>
        /// <param name="items">Category items</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use default value of the default item text</param>
        public virtual void PrepareCategories(IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available categories
            var availableCategoryItems = SelectListHelper.GetCategoryList(_categoryService, _cacheManager, true);
            foreach (var categoryItem in availableCategoryItems)
            {
                items.Add(categoryItem);
            }

            //insert special item for the default value
            PrepareDefaultItem(items, withSpecialDefaultItem, defaultItemText);
        }


        /// <summary>
        /// Prepare available log levels
        /// </summary>
        /// <param name="items">Log level items</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use default value of the default item text</param>
        public virtual void PrepareLogLevels(IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available log levels
            var availableLogLevelItems = LogLevel.Debug.ToSelectList(false);
            foreach (var logLevelItem in availableLogLevelItems)
            {
                items.Add(logLevelItem);
            }

            //insert special item for the default value
            PrepareDefaultItem(items, withSpecialDefaultItem, defaultItemText);
        }

        /// <summary>
        /// Prepare available product types
        /// </summary>
        /// <param name="items">Product type items</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use default value of the default item text</param>
        public virtual void PrepareQuestionTypes(IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available product types
            var availableQuestionTypeItems = QuestionType.SimpleQuestion.ToSelectList(false);
            foreach (var questionTypeItem in availableQuestionTypeItems)
            {
                items.Add(questionTypeItem);
            }

            //insert special item for the default value
            PrepareDefaultItem(items, withSpecialDefaultItem, defaultItemText);
        }

        #endregion
    }
}
