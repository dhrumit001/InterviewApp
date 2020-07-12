
using App.Web.Framework.Models;

namespace App.Web.Models.Categorize
{
    public class CategoryQuestionModel : BaseEntityModel
    {
        #region Properties

        public int CategoryId { get; set; }

        public int QuestionId { get; set; }

        public string QuestionName { get; set; }
        public string AnswerDescription { get; set; }

        public int DisplayOrder { get; set; }

        #endregion

    }
}
