using App.Web.Framework.Models;
using System.ComponentModel;

namespace App.Web.Areas.Admin.Models.Settings
{
    /// <summary>
    /// Represents a setting search model
    /// </summary>
    public partial class SettingSearchModel : BaseSearchModel
    {
        #region Ctor

        public SettingSearchModel()
        {
            AddSetting = new SettingModel();
        }

        #endregion

        #region Properties

        [DisplayName("Name")]
        public string SearchSettingName { get; set; }

        [DisplayName("Value")]
        public string SearchSettingValue { get; set; }
        public SettingModel AddSetting { get; set; }

        #endregion
    }
}
