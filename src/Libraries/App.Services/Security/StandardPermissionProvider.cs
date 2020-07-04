
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
        public static readonly PermissionRecord ManageSettings = new PermissionRecord { Name = "Admin area. Manage Settings", SystemName = "ManageSettings", Category = "Configuration" };
        public static readonly PermissionRecord ManageCategories = new PermissionRecord { Name = "Admin area. Manage Categories", SystemName = "ManageCategories", Category = "Categorize" };
        public static readonly PermissionRecord ManageQuestions = new PermissionRecord { Name = "Admin area. Manage Questions", SystemName = "ManageQuestions", Category = "Categorize" };
    }
}
