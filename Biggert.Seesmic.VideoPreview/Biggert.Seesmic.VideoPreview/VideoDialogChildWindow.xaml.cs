using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;

namespace Biggert.Seesmic.VideoPreview
{
    public partial class VideoDialogChildWindow : ChildWindow
    {
        #region Properties
        public Uri VideoURI
        {
            get { return (Uri)GetValue(VideoURIProperty); }
            set { SetValue(VideoURIProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VideoURI.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VideoURIProperty =
            DependencyProperty.Register("VideoURI", typeof(Uri), typeof(VideoDialogChildWindow), new PropertyMetadata(null));



        public string VideoID
        {
            get { return (string)GetValue(VideoIDProperty); }
            set { SetValue(VideoIDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VideoID.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VideoIDProperty =
            DependencyProperty.Register("VideoID", typeof(string), typeof(VideoDialogChildWindow), new PropertyMetadata(null));


        public Provider Provider
        {
            get { return (Provider)GetValue(ProviderProperty); }
            set { SetValue(ProviderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Provider.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProviderProperty =
            DependencyProperty.Register("Provider", typeof(Provider), typeof(VideoDialogChildWindow), new PropertyMetadata(null));

        
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiates a new instance of VideoDialogChildWindow
        /// </summary>
        public VideoDialogChildWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Events
        /// <summary>
        /// Loaded event for Child Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChildWindow_Loaded(object sender, RoutedEventArgs e)
        {
            switch (Provider)
            {
                case Provider.YouTube:
                    VideoBrowser.NavigateToString(string.Format("<object width=\"425\" height=\"344\"><param name=\"movie\" value=\"http://www.youtube.com/v/{0}&hl=en_US&fs=1&rel=0\"></param><param name=\"allowFullScreen\" value=\"true\"></param><param name=\"allowscriptaccess\" value=\"always\"></param><embed src=\"http://www.youtube.com/v/{0}&hl=en_US&fs=1&rel=0\" type=\"application/x-shockwave-flash\" allowscriptaccess=\"always\" allowfullscreen=\"true\" width=\"425\" height=\"344\"></embed></object>", VideoID));
                    break;
                case Provider.TwitVid:
                    VideoBrowser.NavigateToString(string.Format("<iframe class=\"twitvid-player\" src=\"http://www.twitvid.com/embed.php?guid={0}&autoplay=0\" width=\"425\" height=\"344\" frameborder=\"0\"></iframe>", VideoID));
                    break;
                case VideoPreview.Provider.Vimeo:
                    VideoBrowser.NavigateToString(string.Format("<iframe src=\"http://player.vimeo.com/video/{0}?title=0&amp;byline=0&amp;portrait=0\" width=\"425\" height=\"344\" frameborder=\"0\"></iframe>", VideoID));
                    break;
                default:
                    break;
            }

        }

        #endregion

        private void ChildWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            VideoBrowser.NavigateToString("<HTML>Closing dialog per user request</HTML>");
        }


    }
}

