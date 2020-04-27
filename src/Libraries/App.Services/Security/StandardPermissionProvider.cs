
using App.Core.Domain.Security;

namespace App.Services.Security
{
    /// <summary>
    /// Standard permission provider
    /// </summary>
    public class StandardPermission
    {
        //admin area permissions
        public static readonly PermissionRecord AccessAdminPanel = new PermissionRecord { Name = "Access admin area", SystemName = "AccessAdminPanel", Category = "Standard" };
        public static readonly PermissionRecord ManageSystemLog = new PermissionRecord { Name = "Admin area. Manage System Log", SystemName = "ManageSystemLog", Category = "Configuration" };
    }
}
