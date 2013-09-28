using System;
using System.ComponentModel.Composition;
using System.Windows;
using Seesmic.Sdp.Extensibility;
using Seesmic.Sdp.Utils;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Net;
using System.Windows.Browser;
using System.Runtime.Serialization.Json;
using System.ComponentModel;

namespace Biggert.Seesmic.VideoPreview
{
    [Export(typeof(ITimelineItemAttachment))]
    public class VideoPreviewAttachment : ObservableObject, ITimelineItemAttachment
    {
        private string _text = "View Thumbnail";

        public RelayCommand DialogViewCommand
        {
            get;
            private set;
        }


        public string CollapsedText
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
                this.OnPropertyChanged("CollapsedText");
            }
        }

        public DataTemplate ContentTemplate
        {
            get
            {
                return VideoPreviewPlugin.VideoPreviewTemplate;
            }
        }

        public bool IsInitiallyExpanded
        {
            get
            {
                return VideoPreviewPlugin.AutoShowVideos;
            }
        }

        public DataTemplate LargeLogo
        {
            get
            {
                return (DataTemplate)VideoPreviewPlugin.TemplateResources["YoutubeSmallLogoTemplate"];
            }
        }

        public DataTemplate SmallLogo
        {
            get
            {
                return (DataTemplate)VideoPreviewPlugin.TemplateResources[Provider.ToString() + "SmallLogoTemplate"];
            }
        }

        public Uri VideoURI { get; set; }

        public string VideoID { get; set; }

        public Provider Provider { get; set; }

        private Uri _thumbnailURI;
        public Uri ThumbnailURI
        {
            get
            {
                return _thumbnailURI;
            }
            set
            {
                _thumbnailURI = value;

                this.OnPropertyChanged("ThumbnailURI");
            }
        }

        public VideoPreviewAttachment()
            : base()
        {
            DialogViewCommand = new RelayCommand(ShowVideoDialog);
        }

        private void ShowVideoDialog()
        {
            VideoDialogChildWindow dialog = new VideoDialogChildWindow();
            dialog.VideoURI = VideoURI;
            dialog.VideoID = VideoID;
            dialog.Provider = Provider;
            dialog.Show();
        }

        public void GetThumbnailURI(Provider provider, string videoID)
        {
            switch (provider)
            {
                case VideoPreview.Provider.Vimeo:
                    WebClient client = new WebClient();
                    string requestURL = string.Format("http://vimeo.com/api/v2/video/{0}.json", HttpUtility.UrlEncode(videoID));
                    client.OpenReadCompleted += new OpenReadCompletedEventHandler(OnGetThumbnailURICompleted);
                    client.OpenReadAsync(new Uri(requestURL), ThumbnailURI);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Called when [expand bitly URL completed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing the event data.</param>
        private void OnGetThumbnailURICompleted(object sender, OpenReadCompletedEventArgs args)
        {
            if (args.Error == null)
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(VimeoResponse[]));
                try
                {
                    VimeoResponse[] response;
                    response = serializer.ReadObject(args.Result) as VimeoResponse[];

                    if (response != null && response.Length > 0)
                    {
                        ThumbnailURI = new Uri(response[0].thumbnail_medium);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
