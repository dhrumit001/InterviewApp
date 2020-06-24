using App.Web.Framework.Models;
using System.ComponentModel;

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

        [DisplayName("Setting name")]
        public string Name { get; set; }

        [DisplayName("Value")]
        public string Value { get; set; }
        #endregion
    }
}
