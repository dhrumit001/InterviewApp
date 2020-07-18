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
using App.Web.Framework.Mvc;
using App.Web.Framework.Controllers;
using System.Linq;

namespace App.Web.Areas.Admin.Controllers
{
    public partial class CategoryController : BaseAdminController
    {
        #region Fields

        private readonly ICategoryModelFactory _categoryModelFactory;
        private readonly ICategoryService _categoryService;
        private readonly IQuestionService _questionService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public CategoryController(ICategoryModelFactory categoryModelFactory,
            ICategoryService categoryService,
            IQuestionService questionService,
            INotificationService notificationService,
            IPermissionService permissionService,
            IPictureService pictureService,
            IWorkContext workContext)
        {
            _categoryModelFactory = categoryModelFactory;
            _categoryService = categoryService;
            _questionService = questionService;
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

        #region Questions

        [HttpPost]
        public virtual IActionResult QuestionList(CategoryQuestionSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageQuestions))
                return AccessDeniedDataTablesJson();

            //try to get a category with the specified id
            var category = _categoryService.GetCategoryById(searchModel.CategoryId)
                ?? throw new ArgumentException("No category found with the specified id");

            //prepare model
            var model = _categoryModelFactory.PrepareCategoryQuestionListModel(searchModel, category);

            return Json(model);
        }

        public virtual IActionResult QuestionUpdate(CategoryQuestionModel model)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageCategories))
                return AccessDeniedView();

            //try to get a product category with the specified id
            var questionCategory = _categoryService.GetQuestionCategoryById(model.Id)
                ?? throw new ArgumentException("No question category mapping found with the specified id");

            //fill entity from product
            questionCategory = model.ToEntity(questionCategory);
            _categoryService.UpdateQuestionCategory(questionCategory);

            return new NullJsonResult();
        }

        public virtual IActionResult QuestionDelete(int id)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageCategories))
                return AccessDeniedView();

            //try to get a product category with the specified id
            var questionCategory = _categoryService.GetQuestionCategoryById(id)
                ?? throw new ArgumentException("No question category mapping found with the specified id", nameof(id));

            _categoryService.DeleteQuestionCategory(questionCategory);

            return new NullJsonResult();
        }

        public virtual IActionResult QuestionAddPopup(int categoryId)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageCategories))
                return AccessDeniedView();

            //prepare model
            var model = _categoryModelFactory.PrepareAddQuestionToCategorySearchModel(new AddQuestionToCategorySearchModel());

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult QuestionAddPopupList(AddQuestionToCategorySearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageCategories))
                return AccessDeniedDataTablesJson();

            //prepare model
            var model = _categoryModelFactory.PrepareAddQuestionToCategoryListModel(searchModel);

            return Json(model);
        }

        [HttpPost]
        [FormValueRequired("save")]
        public virtual IActionResult QuestionAddPopup(AddQuestionToCategoryModel model)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageCategories))
                return AccessDeniedView();

            //get selected products
            var selectedQuestions = _questionService.GetQuestionsByIds(model.SelectedQuestionIds.ToArray());
            if (selectedQuestions.Any())
            {
                var existingProductCategories = _categoryService.GetQuestionCategoriesByCategoryId(model.CategoryId, showHidden: true);
                foreach (var product in selectedQuestions)
                {
                    //whether product category with such parameters already exists
                    if (_categoryService.FindQuestionCategory(existingProductCategories, product.Id, model.CategoryId) != null)
                        continue;

                    //insert the new product category mapping
                    _categoryService.InsertQuestionCategory(new QuestionCategory
                    {
                        CategoryId = model.CategoryId,
                        QuestionId = product.Id,
                        DisplayOrder = 1
                    });
                }
            }

            ViewBag.RefreshPage = true;

            return View(new AddQuestionToCategorySearchModel());
        }

        #endregion
    }
}