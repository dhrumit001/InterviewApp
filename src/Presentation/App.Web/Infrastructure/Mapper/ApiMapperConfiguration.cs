using App.Core.Domain.Categorize;
using App.Core.Infrastructure.Mapper;
using App.Web.Models.Categorize;
using AutoMapper;

namespace App.Web.Infrastructure.Mapper
{
    /// <summary>
    /// AutoMapper configuration for admin area models
    /// </summary>
    public class ApiMapperConfiguration : Profile, IOrderedMapperProfile
    {
        #region Ctor

        public ApiMapperConfiguration()
        {
            CreateCategorizeMaps();
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Create catalog maps 
        /// </summary>
        protected virtual void CreateCategorizeMaps()
        {
            CreateMap<QuestionCategory, CategoryQuestionModel>()
            .ForMember(model => model.QuestionName, options => options.Ignore());

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
