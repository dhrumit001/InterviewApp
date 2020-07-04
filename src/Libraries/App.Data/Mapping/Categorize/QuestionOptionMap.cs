using App.Core.Domain.Categorize;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Mapping.Categorize
{
    /// <summary>
    /// Represents a question option mapping configuration
    /// </summary>
    public class QuestionOptionMap : AppEntityTypeConfiguration<QuestionOption>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<QuestionOption> builder)
        {
            builder.ToTable(AppMappingDefaults.QuestionQuestionOptionTable);
            builder.HasKey(question => question.Id);

            builder.Property(question => question.Description).IsRequired();

            base.Configure(builder);
        }

        #endregion
    }
}
