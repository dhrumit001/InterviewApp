using App.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.Web.Areas.Admin.Models.Logging
{
    /// <summary>
    /// Represents a log search model
    /// </summary>
    public class LogSearchModel : BaseSearchModel
    {
        #region Ctor

        public LogSearchModel()
        {
            AvailableLogLevels = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        [DisplayName("Created from")]
        [UIHint("DateNullable")]
        public DateTime? CreatedOnFrom { get; set; }

        [DisplayName("Created to")]
        [UIHint("DateNullable")]
        public DateTime? CreatedOnTo { get; set; }

        [DisplayName("Message")]
        public string Message { get; set; }

        [DisplayName("Log level")]
        public int LogLevelId { get; set; }

        public IList<SelectListItem> AvailableLogLevels { get; set; }

        #endregion
    }
}
