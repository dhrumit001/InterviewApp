using App.Core.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Mapping.Users
{
    /// <summary>
    /// Represents a user mapping configuration
    /// </summary>
    public partial class UserMap : AppEntityTypeConfiguration<User>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(user => user.Id);

            builder.Property(user => user.Username).HasMaxLength(1000);
            builder.Property(user => user.Email).HasMaxLength(1000);
            builder.Property(user => user.EmailToRevalidate).HasMaxLength(1000);
            
            base.Configure(builder);
        }

        #endregion
    }
}
