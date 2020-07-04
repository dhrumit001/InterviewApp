using App.Core.Domain.Categorize;
using App.Core.Domain.Logging;
using App.Core.Infrastructure.Mapper;
using App.Web.Areas.Admin.Models.Categorize;
using App.Web.Areas.Admin.Models.Logging;
using App.Web.Framework.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Areas.Admin.Infrastructure.Mapper
{
    /// <summary>
    /// AutoMapper configuration for admin area models
    /// </summary>
    public class AdminMapperConfiguration : Profile, IOrderedMapperProfile
    {
        #region Ctor

        public AdminMapperConfiguration()
        {
            //create specific maps
            CreateLoggingMaps();

            CreateCategorizeMaps();

            //add some generic mapping rules
            ForAllMaps((mapConfiguration, map) =>
            {
                //exclude Form and CustomProperties from mapping BaseNopModel
                if (typeof(BaseModel).IsAssignableFrom(mapConfiguration.DestinationType))
                {
                    //map.ForMember(nameof(BaseNopModel.Form), options => options.Ignore());
                    // map.ForMember(nameof(BaseModel.CustomProperties), options => options.Ignore());
                }

            });
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Create logging maps 
        /// </summary>
        protected virtual void CreateLoggingMaps()
        {

            CreateMap<Log, LogModel>()
                .ForMember(model => model.CreatedOn, options => options.Ignore())
                .ForMember(model => model.FullMessage, options => options.Ignore())
                .ForMember(model => model.UserEmail, options => options.Ignore());
            CreateMap<LogModel, Log>()
                .ForMember(entity => entity.CreatedOnUtc, options => options.Ignore())
                .ForMember(entity => entity.User, options => options.Ignore())
                .ForMember(entity => entity.LogLevelId, options => options.Ignore());
        }

        /// <summary>
        /// Create catalog maps 
        /// </summary>
        protected virtual void CreateCategorizeMaps()
        {

            CreateMap<Category, CategoryModel>()
            .ForMember(model => model.AvailableCategories, options => options.Ignore())
            .ForMember(model => model.Breadcrumb, options => options.Ignore());

            CreateMap<CategoryModel, Category>()
            .ForMember(entity => entity.CreatedOnUtc, options => options.Ignore())
            .ForMember(entity => entity.Deleted, options => options.Ignore())
            .ForMember(entity => entity.UpdatedOnUtc, options => options.Ignore());

            //products
            CreateMap<Question, QuestionModel>()
            .ForMember(model => model.QuestionTypeName, options => options.Ignore());

            CreateMap<QuestionModel, Question>()
            .ForMember(entity => entity.CreatedOnUtc, options => options.Ignore())
            .ForMember(entity => entity.Deleted, options => options.Ignore())
            .ForMember(entity => entity.UpdatedOnUtc, options => options.Ignore());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Order of this mapper implementation
        /// </summary>
        public int Order => 0;

        #endregion
    }
}
