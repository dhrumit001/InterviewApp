
namespace App.Core.Domain.Categorize
{
    /// <summary>
    /// Represents a question category mapping
    /// </summary>
    public class QuestionCategory : BaseEntity
    {
        /// <summary>
        /// Gets or sets the question identifier
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// Gets or sets the category identifier
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets the category
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// Gets the question
        /// </summary>
        public virtual Question Question { get; set; }
    }
}
