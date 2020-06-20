﻿using App.Core.Domain.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Mapping.Configuration
{
    /// <summary>
    /// Represents a setting mapping configuration
    /// </summary>
    public partial class SettingMap : AppEntityTypeConfiguration<Setting>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.ToTable(nameof(Setting));
            builder.HasKey(setting => setting.Id);

            builder.Property(setting => setting.Name).HasMaxLength(200).IsRequired();
            builder.Property(setting => setting.Value).IsRequired();

            base.Configure(builder);
        }

        #endregion
    }
}
