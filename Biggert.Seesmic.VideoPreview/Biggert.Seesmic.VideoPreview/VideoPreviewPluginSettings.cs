using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Seesmic.Sdp.Extensibility;
using System.ComponentModel.Composition;

namespace Biggert.Seesmic.VideoPreview
{
    /// <summary>
    /// Plugin settings
    /// </summary>
    public class VideoPreviewPluginSettings
    {
        #region PROPERTIES
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether [auto show videos].
        /// </summary>
        /// <value><c>true</c> if [auto show videos]; otherwise, <c>false</c>.</value>
        public bool AutoShowVideos { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether [auto process short UR ls].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [auto process short UR ls]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoProcessShortURLs { get; private set; }

        /// <summary>
        /// Gets or sets the bitly login.
        /// </summary>
        /// <value>The bitly login.</value>
        public string BitlyLogin { get; private set; }

        /// <summary>
        /// Gets or sets the bitly API key.
        /// </summary>
        /// <value>The bitly API key.</value>
        public string BitlyAPIKey { get; private set; }
        
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of the <see cref="VideoPreviewPluginSettings"/> class.
        /// </summary>
        public VideoPreviewPluginSettings()
        {

            // Get all the values from storage
            IStorageService StorageService = VideoPreviewPlugin.StorageService;

            Id = new Guid("bc32900d-a9d0-4ac1-a684-98fb9a4d2c19");
            //AutoShowVideos = StorageService.GetValue<bool>(this.Id, "AutoShowVideos", true);
            //AutoProcessShortURLs = StorageService.GetValue<bool>(this.Id, "AutoProcessShortURLs", true);
            //BitlyLogin = StorageService.GetValue<string>(this.Id, "BitlyLogin", "biggert");
            //BitlyAPIKey = StorageService.GetValue<string>(this.Id, "BitlyAPIKey", "R_ae6cf0443eb4f6c30ffbbcb2b22e63aa");

        }

        #endregion
       
    }
}
