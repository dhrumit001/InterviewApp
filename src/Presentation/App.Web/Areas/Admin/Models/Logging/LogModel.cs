using App.Web.Framework.Models;
using System;
using System.ComponentModel;

namespace App.Web.Areas.Admin.Models.Logging
{
    /// <summary>
    /// Represents a log model
    /// </summary>
    public partial class LogModel : BaseEntityModel
    {
        #region Properties

        [DisplayName("Log level")]
        public string LogLevel { get; set; }

        [DisplayName("Short message")]
        public string ShortMessage { get; set; }

        [DisplayName("Full message")]
        public string FullMessage { get; set; }

        [DisplayName("IP address")]
        public string IpAddress { get; set; }

        [DisplayName("User")]
        public int? UserId { get; set; }

        [DisplayName("User")]
        public string UserEmail { get; set; }

        [DisplayName("Page URL")]
        public string PageUrl { get; set; }

        [DisplayName("Referrer URL")]
        public string ReferrerUrl { get; set; }

        [DisplayName("Created on")]
        public DateTime CreatedOn { get; set; }

        #endregion
    }
}
