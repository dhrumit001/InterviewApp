using App.Core.Configuration;

namespace App.Core.Domain.Media
{
    /// <summary>
    /// Media settings
    /// </summary>
    public class MediaSettings : ISettings
    {
        /// <summary>
        /// Picture size of customer avatars (if enabled)
        /// </summary>
        public int AvatarPictureSize { get; set; }

        /// <summary>
        /// Picture size of category pictures
        /// </summary>
        public int CategoryThumbPictureSize { get; set; }

        /// <summary>
        /// Picture size of product pictures for autocomplete search box
        /// </summary>
        public int AutoCompleteSearchThumbPictureSize { get; set; }

        /// <summary>
        /// Picture size of image squares on a product details page (used with "image squares" attribute type
        /// </summary>
        public int ImageSquarePictureSize { get; set; }

        /// <summary>
        /// Maximum allowed picture size. If a larger picture is uploaded, then it'll be resized
        /// </summary>
        public int MaximumImageSize { get; set; }

        /// <summary>
        /// Gets or sets a default quality used for image generation
        /// </summary>
        public int DefaultImageQuality { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether single (/content/images/thumbs/) or multiple (/content/images/thumbs/001/ and /content/images/thumbs/002/) directories will used for picture thumbs
        /// </summary>
        public bool MultipleThumbDirectories { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether need to use absolute pictures path
        /// </summary>
        public bool UseAbsoluteImagePath { get; set; }
    }
}