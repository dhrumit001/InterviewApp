using App.Core.Domain.Categorize;
using App.Services.Categorize;
using App.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using App.Web.Framework.Controllers;
using App.Web.Models.Categorize;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : BaseController
    {
        #region Fields

        private readonly ICategoryService _categoryService;
        private readonly IQuestionService _questionService;

        #endregion

        #region Ctor

        public CategoryController(ICategoryService categoryService,
            IQuestionService questionService)
        {
            _categoryService = categoryService;
            _questionService = questionService;
        }

        #endregion

        #region List

        [HttpGet]
        public IList<CategoryModel> List()
        {
            var categories = _categoryService.GetAllCategories();

            //prepare model
            var model = categories.Select(category =>
            {
                //fill in model values from the entity
                var categoryQuestionModel = category.ToModel<CategoryModel>();
                return categoryQuestionModel;

            }).ToList();

            return model;
        }

        #region Questions

        [HttpGet("{id}/questions")]
        public virtual IList<CategoryQuestionModel> QuestionList(int id)
        {
            //try to get a category with the specified id
            var category = _categoryService.GetCategoryById(id)
                ?? throw new ArgumentException("No category found with the specified id");

            //get question categories
            var questionCategories = _categoryService.GetQuestionCategoriesByCategoryId(category.Id, showHidden: true);

            //prepare model
            var model = questionCategories.Select(questionCategory =>
            {
                //fill in model values from the entity
                var categoryQuestionModel = questionCategory.ToModel<CategoryQuestionModel>();

                var question = _questionService.GetQuestionById(questionCategory.QuestionId);
                //fill in additional values (not existing in the entity)
                categoryQuestionModel.QuestionName = question?.Name;
                categoryQuestionModel.AnswerDescription = question?.QuestionAnswer?.Description;

                return categoryQuestionModel;
            }).ToList();

            return model;
        }

        #endregion

        #endregion

    }
}