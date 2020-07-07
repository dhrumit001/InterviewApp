using App.Core;
using App.Core.Domain.Categorize;
using App.Core.Infrastructure;
using App.Services.Categorize;
using App.Services.Messages;
using App.Services.Security;
using App.Web.Areas.Admin.Factories;
using App.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using App.Web.Areas.Admin.Models.Categorize;
using App.Web.Framework.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Web.Areas.Admin.Controllers
{
    public partial class QuestionController : BaseAdminController
    {
        #region Fields

        private readonly ICategoryService _categoryService;
        private readonly IAppFileProvider _fileProvider;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IQuestionModelFactory _questionModelFactory;
        private readonly IQuestionService _questionService;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public QuestionController(ICategoryService categoryService,
            IAppFileProvider fileProvider,
            INotificationService notificationService,
            IPermissionService permissionService,
            IQuestionModelFactory questionModelFactory,
            IQuestionService questionService,
            IWorkContext workContext)
        {

            _categoryService = categoryService;
            _fileProvider = fileProvider;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _questionModelFactory = questionModelFactory;
            _questionService = questionService;
            _workContext = workContext;
        }

        #endregion

        #region Utilities

        protected virtual void SaveCategoryMappings(Question question, QuestionModel model)
        {
            var existingProductCategories = _categoryService.GetQuestionCategoriesByQuestionId(question.Id, true);

            //delete categories
            foreach (var existingProductCategory in existingProductCategories)
                if (!model.SelectedCategoryIds.Contains(existingProductCategory.CategoryId))
                    _categoryService.DeleteQuestionCategory(existingProductCategory);

            //add categories
            foreach (var categoryId in model.SelectedCategoryIds)
            {
                if (_categoryService.FindQuestionCategory(existingProductCategories, question.Id, categoryId) == null)
                {
                    //find next display order
                    var displayOrder = 1;
                    var existingCategoryMapping = _categoryService.GetQuestionCategoriesByCategoryId(categoryId, showHidden: true);
                    if (existingCategoryMapping.Any())
                        displayOrder = existingCategoryMapping.Max(x => x.DisplayOrder) + 1;
                    _categoryService.InsertQuestionCategory(new QuestionCategory
                    {
                        QuestionId = question.Id,
                        CategoryId = categoryId,
                        DisplayOrder = displayOrder
                    });
                }
            }
        }

        #endregion

        #region Methods

        #region Question list / create / edit / delete

        public virtual IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual IActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermission.ManageQuestions))
                return AccessDeniedView();

            //prepare model
            var model = _questionModelFactory.PrepareQuestionSearchModel(new QuestionSearchModel());

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult QuestionList(QuestionSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageQuestions))
                return AccessDeniedDataTablesJson();

            //prepare model
            var model = _questionModelFactory.PrepareQuestionListModel(searchModel);

            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermission.ManageQuestions))
                return AccessDeniedView();

            //prepare model
            var model = _questionModelFactory.PrepareQuestionModel(new QuestionModel(), null);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(QuestionModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageQuestions))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {

                //question
                var question = model.ToEntity<Question>();
                question.CreatedOnUtc = DateTime.UtcNow;
                question.UpdatedOnUtc = DateTime.UtcNow;
                _questionService.InsertQuestion(question);

                //add question answer
                _questionService.InsertQuestionOption(new QuestionOption
                {
                    QuestionId = question.Id,
                    Description = model.AnswerDescription,
                    IsAnswer = true
                });

                //categories
                SaveCategoryMappings(question, model);

                _notificationService.SuccessNotification("The new question has been added successfully.");

                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = question.Id });
            }

            //prepare model
            model = _questionModelFactory.PrepareQuestionModel(model, null, true);

            //if we got this far, something failed, redisplay form
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageQuestions))
                return AccessDeniedView();

            //try to get a question with the specified id
            var question = _questionService.GetQuestionById(id);
            if (question == null || question.Deleted)
                return RedirectToAction("List");

            //prepare model
            var model = _questionModelFactory.PrepareQuestionModel(null, question);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Edit(QuestionModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageQuestions))
                return AccessDeniedView();

            //try to get a question with the specified id
            var question = _questionService.GetQuestionById(model.Id);
            if (question == null || question.Deleted)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {

                //question
                question = model.ToEntity(question);

                question.UpdatedOnUtc = DateTime.UtcNow;
                _questionService.UpdateQuestion(question);

                //add question answer
                _questionService.UpdateQuestionOption(new QuestionOption
                {
                    QuestionId = question.Id,
                    Description = model.AnswerDescription,
                    IsAnswer = true
                });

                //categories
                SaveCategoryMappings(question, model);

                _notificationService.SuccessNotification("The question has been updated successfully.");

                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = question.Id });
            }

            //prepare model
            model = _questionModelFactory.PrepareQuestionModel(model, question, true);

            //if we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageQuestions))
                return AccessDeniedView();

            //try to get a product with the specified id
            var product = _questionService.GetQuestionById(id);
            if (product == null)
                return RedirectToAction("List");

            _questionService.DeleteQuestion(product);

            _notificationService.SuccessNotification("The question has been deleted successfully.");

            return RedirectToAction("List");
        }

        [HttpPost]
        public virtual IActionResult DeleteSelected(ICollection<int> selectedIds)
        {
            if (!_permissionService.Authorize(StandardPermission.ManageQuestions))
                return AccessDeniedView();

            if (selectedIds != null)
            {
                _questionService.DeleteQuestions(_questionService.GetQuestionsByIds(selectedIds.ToArray()));
            }

            return Json(new { Result = true });
        }

        #endregion

        #endregion
    }
}
