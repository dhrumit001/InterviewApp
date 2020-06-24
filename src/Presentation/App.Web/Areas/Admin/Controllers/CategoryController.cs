using System;
using App.Core;
using App.Services.Categorize;
using App.Services.Messages;
using App.Services.Security;
using Microsoft.AspNetCore.Mvc;
using App.Services.Media;
using App.Core.Domain.Categorize;
using App.Web.Areas.Admin.Models.Categorize;
using App.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using App.Web.Framework.Mvc.Filters;
using App.Web.Areas.Admin.Factories;

namespace App.Web.Areas.Admin.Controllers
{
    public partial class CategoryController : BaseAdminController
    {
        #region Fields

        private readonly ICategoryModelFactory _categoryModelFactory;
        private readonly ICategoryService _categoryService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public CategoryController(ICategoryModelFactory categoryModelFactory,
            ICategoryService categoryService,
            INotificationService notificationService,
            IPermissionService permissionService,
            IPictureService pictureService,
            IWorkContext workContext)
        {
            _categoryModelFactory = categoryModelFactory;
            _categoryService = categoryService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _pictureService = pictureService;
            _workContext = workContext;
        }

        #endregion

        #region List

        public virtual IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual IActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermission.ManageCategories))
                return AccessDeniedView();

            //prepare model
            var model = _categoryModelFactory.PrepareCategorySearchModel(new CategorySearchModel());

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(CategorySearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageCategories))
                return AccessDeniedDataTablesJson();

            //prepare model
            var model = _categoryModelFactory.PrepareCategoryListModel(searchModel);

            return Json(model);
        }

        #endregion

        #region Create / Edit / Delete

        public virtual IActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermission.ManageCategories))
                return AccessDeniedView();

            //prepare model
            var model = _categoryModelFactory.PrepareCategoryModel(new CategoryModel(), null);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(CategoryModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageCategories))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var category = model.ToEntity<Category>();
                category.CreatedOnUtc = DateTime.UtcNow;
                category.UpdatedOnUtc = DateTime.UtcNow;
                _categoryService.InsertCategory(category);

                
                _categoryService.UpdateCategory(category);

                _notificationService.SuccessNotification("The new category has been added successfully.");

                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = category.Id });
            }

            //prepare model
            model = _categoryModelFactory.PrepareCategoryModel(model, null, true);

            //if we got this far, something failed, redisplay form
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageCategories))
                return AccessDeniedView();

            //try to get a category with the specified id
            var category = _categoryService.GetCategoryById(id);
            if (category == null || category.Deleted)
                return RedirectToAction("List");

            //prepare model
            var model = _categoryModelFactory.PrepareCategoryModel(null, category);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Edit(CategoryModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageCategories))
                return AccessDeniedView();

            //try to get a category with the specified id
            var category = _categoryService.GetCategoryById(model.Id);
            if (category == null || category.Deleted)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                var prevPictureId = category.PictureId;

                category = model.ToEntity(category);
                category.UpdatedOnUtc = DateTime.UtcNow;
                _categoryService.UpdateCategory(category);

                //delete an old picture (if deleted or updated)
                if (prevPictureId > 0 && prevPictureId != category.PictureId)
                {
                    var prevPicture = _pictureService.GetPictureById(prevPictureId);
                    if (prevPicture != null)
                        _pictureService.DeletePicture(prevPicture);
                }

                _notificationService.SuccessNotification("The category has been updated successfully.");

                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = category.Id });
            }

            //prepare model
            model = _categoryModelFactory.PrepareCategoryModel(model, category, true);

            //if we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageCategories))
                return AccessDeniedView();

            //try to get a category with the specified id
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
                return RedirectToAction("List");

            _categoryService.DeleteCategory(category);

            _notificationService.SuccessNotification("The category has been deleted successfully.");

            return RedirectToAction("List");
        }

        #endregion

    }
}