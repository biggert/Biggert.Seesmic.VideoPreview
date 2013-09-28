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
using System.Collections.Generic;

namespace Biggert.Seesmic.VideoPreview
{
    [DataContract]
    public class BitlyResponse
    {

        [DataMember(Name = "data")]
        public BitlyData Data { get; set; }

        [DataMember(Name = "status_code")]
        public int Status_Code { get; set; }

    }

    [DataContract]
    public class BitlyData
    {
        [DataMember(Name = "expand")]
        public List<URL> Long_URLs { get; set; }
    }

    [DataContract]
    public class URL
    {
        [DataMember(Name = "long_url")]
        public string Long_URL { get; set; }

    }
}
