using System;
using System.Collections.Generic;

namespace App.Core.Domain.Categorize
{
    /// <summary>
    /// Represents a question
    /// </summary>
    public class Question : BaseEntity
    {
        private ICollection<QuestionOption> _questionOptions;
        private ICollection<QuestionCategory> _questionCategories;

        /// <summary>
        /// Gets or sets the product type identifier
        /// </summary>
        public int QuestionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time of question creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of question update
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }


        /// <summary>
        /// Gets or sets the question type
        /// </summary>
        public QuestionType QuestionType
        {
            get => (QuestionType)QuestionTypeId;
            set => QuestionTypeId = (int)value;
        }

        /// <summary>
        /// Gets or sets the collection of QuestionOption
        /// </summary>
        public virtual ICollection<QuestionOption> QuestionOptions
        {
            get => _questionOptions ?? (_questionOptions = new List<QuestionOption>());
            protected set => _questionOptions = value;
        }

        /// <summary>
        /// Gets or sets the collection of QuestionCategory
        /// </summary>
        public virtual ICollection<QuestionCategory> QuestionCategories
        {
            get => _questionCategories ?? (_questionCategories = new List<QuestionCategory>());
            protected set => _questionCategories = value;
        }
    }
}
