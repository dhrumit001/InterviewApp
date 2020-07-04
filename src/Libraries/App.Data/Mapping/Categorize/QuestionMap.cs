using App.Core.Domain.Categorize;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Mapping.Categorize
{
    /// <summary>
    /// Represents a question mapping configuration
    /// </summary>
    public class QuestionMap : AppEntityTypeConfiguration<Question>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable(nameof(Question));
            builder.HasKey(question => question.Id);

            builder.Property(question => question.Name).HasMaxLength(400).IsRequired();
            
            builder.Ignore(question => question.QuestionType);
            
            base.Configure(builder);
        }

        #endregion
    }
}
