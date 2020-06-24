using App.Core.Configuration;

namespace App.Core.Domain.Common
{
    /// <summary>
    /// Common settings
    /// </summary>
    public class CommonSettings : ISettings
    {
        public CommonSettings()
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether to display a warning if java-script is disabled
        /// </summary>
        public bool DisplayJavaScriptDisabledWarning { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether jQuery migrate script logging is active
        /// </summary>
        public bool JqueryMigrateScriptLoggingActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to compress response (gzip by default). 
        /// You may want to disable it, for example, If you have an active IIS Dynamic Compression Module configured at the server level
        /// </summary>
        public bool UseResponseCompression { get; set; }

        /// <summary>
        /// Gets or sets a value of "Cache-Control" header value for static content
        /// </summary>
        public string StaticFilesCacheControl { get; set; }

        /// <summary>
        /// Gets or sets a value of favicon and app icons <head/> code
        /// </summary>
        public string FaviconAndAppIconsHeadCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to enable markup minification
        /// </summary>
        public bool EnableHtmlMinification { get; set; }

        /// <summary>
        /// A value indicating whether JS file bundling and minification is enabled
        /// </summary>
        public bool EnableJsBundling { get; set; }

        /// <summary>
        /// A value indicating whether CSS file bundling and minification is enabled
        /// </summary>
        public bool EnableCssBundling { get; set; }

    }
}
