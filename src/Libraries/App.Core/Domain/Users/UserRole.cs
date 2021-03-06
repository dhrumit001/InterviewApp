﻿using App.Core.Domain.Security;
using System;
using System.Collections.Generic;

namespace App.Core.Domain.Users
{
   
    /// <summary>
    /// Represents a user role
    /// </summary>
    public partial class UserRole : BaseEntity
    {
        private ICollection<PermissionRecordUserRoleMapping> _permissionRecordUserRoleMappings;

        /// <summary>
        /// Gets or sets the user role name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user role is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user role is system
        /// </summary>
        public bool IsSystemRole { get; set; }

        /// <summary>
        /// Gets or sets the user role system name
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets the permission record-user role mappings
        /// </summary>
        public virtual ICollection<PermissionRecordUserRoleMapping> PermissionRecordUserRoleMappings
        {
            get => _permissionRecordUserRoleMappings ?? (_permissionRecordUserRoleMappings = new List<PermissionRecordUserRoleMapping>());
            protected set => _permissionRecordUserRoleMappings = value;
        }
    }
}
