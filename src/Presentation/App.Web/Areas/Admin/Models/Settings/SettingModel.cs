using App.Web.Framework.Models;

namespace App.Web.Areas.Admin.Models.Settings
{
    /// <summary>
    /// Represents a setting model
    /// </summary>
    public partial class SettingModel : BaseEntityModel
    {
        #region Ctor

        public SettingModel()
        {
        }

        #endregion

        #region Properties

        public string Name { get; set; }
        public string Value { get; set; }
        #endregion
    }
}
