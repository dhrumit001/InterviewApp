using App.Web.Framework.Models;
using System;
namespace App.Web.Areas.Admin.Models.Logging
{
    /// <summary>
    /// Represents a log model
    /// </summary>
    public partial class LogModel : BaseEntityModel
    {
        #region Properties

        public string LogLevel { get; set; }

        public string ShortMessage { get; set; }

        public string FullMessage { get; set; }

        public string IpAddress { get; set; }

        public int? UserId { get; set; }

        public string UserEmail { get; set; }

        public string PageUrl { get; set; }

        public string ReferrerUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        #endregion
    }
}
