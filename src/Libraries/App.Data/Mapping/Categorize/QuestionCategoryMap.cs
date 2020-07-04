using App.Core.Domain.Categorize;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Mapping.Categorize
{
    /// <summary>
    /// Represents a question category mapping configuration
    /// </summary>
    public class QuestionCategoryMap : AppEntityTypeConfiguration<QuestionCategory>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<QuestionCategory> builder)
        {
            builder.ToTable(AppMappingDefaults.QuestionCategoryTable);
            builder.HasKey(question => question.Id);

            builder.HasOne(questionCategory => questionCategory.Category)
                .WithMany()
                .HasForeignKey(questionCategory => questionCategory.CategoryId)
                .IsRequired();

            base.Configure(builder);
        }

        #endregion
    }
}
