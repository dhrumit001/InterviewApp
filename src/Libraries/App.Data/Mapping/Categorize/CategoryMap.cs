using App.Core.Domain.Categorize;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Mapping.Categorize
{
    /// <summary>
    /// Represents a category mapping configuration
    /// </summary>
    public partial class CategoryMap : AppEntityTypeConfiguration<Category>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category));
            builder.HasKey(category => category.Id);

            builder.Property(category => category.Name).HasMaxLength(400).IsRequired();
            
            base.Configure(builder);
        }

        #endregion
    }
}
