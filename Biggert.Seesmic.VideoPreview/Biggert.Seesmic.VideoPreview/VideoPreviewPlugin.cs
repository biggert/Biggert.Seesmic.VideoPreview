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
using System.ComponentModel.Composition;
using Seesmic.Sdp.Extensibility;

namespace Biggert.Seesmic.VideoPreview
{
    [Export(typeof(IPlugin))]
    public class VideoPreviewPlugin : IPlugin
    {
        #region Constructors
        internal static readonly ResourceDictionary TemplateResources;

        public VideoPreviewPlugin() { }
        static VideoPreviewPlugin()
        {
            ResourceDictionary templates = new ResourceDictionary();
            templates.Source = new Uri("/Biggert.Seesmic.VideoPreview;component/Templates/DataTemplates.xaml", UriKind.Relative);
            TemplateResources = templates;
        }
        #endregion

        internal static DataTemplate SmallLogoTemplate
        {
            get
            {
                return (DataTemplate)TemplateResources["SmallLogoTemplate"];
            }
        }

        #region Shell Imports
        private static IShellService _shell;
        private static ILogService _log;
        private static IStorageService _storage;

        [Import]
        public IShellService ShellServiceImport { set { _shell = value; } }

        [Import]
        public ILogService LogServiceImport { set { _log = value; } }

        [Import]
        public IStorageService StorageServiceImport { set { _storage = value; } }

        public static IShellService ShellService { get { return _shell; } }
        public static ILogService LogService { get { return _log; } }
        public static IStorageService StorageService { get { return _storage; } }
        #endregion

        #region IPlugin Implementations
        public void CommitSettings()
        {
        }

        public Guid Id
        {
            get { return new Guid("bc32900d-a9d0-4ac1-a684-98fb9a4d2c19"); }
        }

        public void Initialize()
        {
            _autoShowVideos = StorageService.GetValue<bool>(this.Id, "AutoShowVideos", true);
            _autoProcessShortURLs = StorageService.GetValue<bool>(this.Id, "AutoProcessShortURLs", true);
        }

        public void RevertSettings()
        {
        }

        public DataTemplate SettingsTemplate
        {
            get
            {
                return (DataTemplate)TemplateResources["SettingsTemplate"];
            }
        }
        #endregion

        private static bool _autoShowVideos;

        public static bool AutoShowVideos
        {
            get
            {
                return _autoShowVideos;
            }
            set
            {
                if (value != _autoShowVideos)
                {
                    _autoShowVideos = value;
                    StorageService.SetValue<bool>(new Guid("bc32900d-a9d0-4ac1-a684-98fb9a4d2c19"), "AutoShowVideos", value);
                }
            }
        }

        private static bool _autoProcessShortURLs;

        public static bool AutoProcessShortURLs
        {
            get
            {
                return _autoProcessShortURLs;
            }
            set
            {
                if (value != _autoProcessShortURLs)
                {
                    _autoProcessShortURLs = value;
                    StorageService.SetValue<bool>(new Guid("bc32900d-a9d0-4ac1-a684-98fb9a4d2c19"), "AutoProcessShortURLs", value);
                }
            }
        }

        internal static DataTemplate VideoPreviewTemplate
        {
            get
            {
                return (DataTemplate)TemplateResources["VideoPreviewTemplate"];
            }
        }


    }
}
