
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
using System.Runtime.Serialization;

namespace Biggert.Seesmic.VideoPreview
{
    [DataContract]
    public class UnShortmeResponse
    {
        [DataMember(Name = "requestedURL")]
        public string RequestedURL;

        [DataMember(Name = "success")]
        public bool Success;

        [DataMember(Name = "resolvedURL")]
        public string ResolvedURL;
    }
}
