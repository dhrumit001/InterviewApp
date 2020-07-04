using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Domain.Categorize
{
    /// <summary>
    /// Represents a question
    /// </summary>
    public class QuestionOption : BaseEntity
    {
        /// <summary>
        /// Gets or sets the question identifier
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the option is question answer
        /// </summary>
        public bool IsAnswer { get; set; }

        /// <summary>
        /// Gets the question
        /// </summary>
        public virtual Question Question { get; set; }
    }
}
