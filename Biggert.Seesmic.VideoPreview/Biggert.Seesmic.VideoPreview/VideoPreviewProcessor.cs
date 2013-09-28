using System;
using System.ComponentModel.Composition;
using Seesmic.Sdp.Extensibility;
using System.Text.RegularExpressions;
using System.Net;
using System.Windows;
using System.IO;
using System.Windows.Browser;
using System.Xml.Linq;
using System.Runtime.Serialization.Json;
using System.Linq;
using System.Xml;
using System.Threading;

namespace Biggert.Seesmic.VideoPreview
{
    [Export(typeof(ITimelineItemProcessor))]
    public class VideoPreviewProcessor : ITimelineItemProcessor
    {
        private const string YOUTUBE_EMBEDCODE = "<object width=\"425\" height=\"344\"><param name=\"movie\" value=\"http://www.youtube.com/v/{0}&hl=en_US&fs=1&rel=0\"></param><param name=\"allowFullScreen\" value=\"true\"></param><param name=\"allowscriptaccess\" value=\"always\"></param><embed src=\"http://www.youtube.com/v/{0}&hl=en_US&fs=1&rel=0\" type=\"application/x-shockwave-flash\" allowscriptaccess=\"always\" allowfullscreen=\"true\" width=\"425\" height=\"344\"></embed></object>";
        private const string TWITVID_EMBEDCODE = "<object width=\"425\" height=\"344\"><param name=\"movie\" value=\"http://www.twitvid.com/player/{0}\"></param><param name=\"allowscriptaccess\" value=\"always\"></param><param name=\"allowFullScreen\" value=\"true\"></param><embed type=\"application/x-shockwave-flash\" src=\"http://www.twitvid.com/player/{0}\" quality=\"high\" allowscriptaccess=\"always\" allowNetworking=\"all\" allowfullscreen=\"true\" wmode=\"transparent\" height=\"344\" width=\"425\"></object>";
        private const string YOUTUBE_URIREGEX = @"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)";
        //private const string YOUTUBESHORT_URIREGEX = "http://youtu.be/(?<VideoID>[0-9a-zA-Z\\-_]*)";
        private const string TWITVID_URIREGEX = @"twitvid\.com/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)";
        private const string VIMEO_URIREGEX = @"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)";
        //private const string BITLY_SHORTURLS_URIREGEX = "http://(4sq.com|amzn.to|binged.it|bit.ly|j.mp|nyti.ms|huff.to|pep.si|n.pr|tcrn.ch)/(?<Id>[0-9a-zA-Z]*)";
        private const string MIGREME_SHORTURLS_URIREGEX = "http://migre.me/(?<Id>[0-9a-zA-Z]*)";
        private const string UNSHORTME_SHORTURLS_URIREGEX = "http://(tinyurl.com|tr.im|cli.gs|ow.ly|snurl.com|kl.am|poprl.com|idek.net|budurl.com|is.gd|hapylink.com|4sq.com|amzn.to|binged.it|bit.ly|j.mp|nyti.ms|huff.to|pep.si|n.pr|tcrn.ch)/(?<Id>[0-9a-zA-Z]*)";
        private const string URI_REGEX = @"[\w-_]+(\.[\w-_]+)+([\w-.,@?^=%&:/~+#]*[\w-\@?^=%&/~+#])?";
        private VideoPreviewPluginSettings _settings = new VideoPreviewPluginSettings();

        public void Deliver(TimelineItemContainer timelineItemContainer)
        {
        }

        public bool Filter(TimelineItemContainer timelineItemContainer)
        {
            return true;
        }

        public void Process(TimelineItemContainer timelineItemContainer)
        {
            string itemText = timelineItemContainer.TimelineItem.Text;
            Regex regex;

            regex = new Regex(URI_REGEX);

            if (regex.IsMatch(itemText))
            {
                MatchCollection matches = new Regex(URI_REGEX).Matches(itemText);

                foreach (Match match in matches)
                {
                    string url = match.Value;
                    WebClient client = new WebClient();
                    string requestURL = url.Trim().ToUpperInvariant().StartsWith(@"HTTP://") ? url.Trim() : @"http://" + url.Trim();
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestURL);

                    // Start the asynchronous request.
                    IAsyncResult result = (IAsyncResult)request.BeginGetResponse(new AsyncCallback(RespCallback), new object[] { request, timelineItemContainer });
                }
            }

            //// Check for migre.me
            //regex = new Regex(MIGREME_SHORTURLS_URIREGEX);
            //// Check for short URL service(s) and send the response accordingly
            //if (_settings.AutoProcessShortURLs && regex.IsMatch(itemText))
            //{
            //    string shortURL = regex.Match(itemText).Value;
            //    WebClient client = new WebClient();
            //    string requestURL = string.Format("http://migre.me/api_redirect2.xml?url={0}", HttpUtility.UrlEncode(shortURL.Trim()));
            //    client.OpenReadCompleted += new OpenReadCompletedEventHandler(OnExpandMigreMeURLCompleted);
            //    client.OpenReadAsync(new Uri(requestURL), timelineItemContainer);
            //}
            //else
            //{
            //    // Check for unshort.me

            //    // Set up the regex for the different services
            //    regex = new Regex(UNSHORTME_SHORTURLS_URIREGEX);
            //    // Check for short URL service(s) and send the response accordingly
            //    if (_settings.AutoProcessShortURLs && regex.IsMatch(itemText))
            //    {
            //        string apiKey = _settings.BitlyAPIKey;
            //        string apiLogin = _settings.BitlyLogin;
            //        string shortURL = regex.Match(itemText).Value;
            //        WebClient client = new WebClient();
            //        //string requestURL = string.Format("http://api.bit.ly/v3/expand?login=" + apiLogin + "&apiKey=" + apiKey + "&shortUrl={0}&format=json", HttpUtility.UrlEncode(shortURL.Trim()));
            //        string requestURL = string.Format("http://api.unshort.me/?r={0}&t=json", HttpUtility.UrlEncode(shortURL.Trim()));
            //        client.OpenReadCompleted += new OpenReadCompletedEventHandler(OnExpandUnShortMeURLCompleted);
            //        client.OpenReadAsync(new Uri(requestURL), timelineItemContainer);
            //    }
            //    else
            //    {
            //        // We know it's not a Short URL service so let's run the basic checks asynchronously
            //        Thread threadRequest = new Thread(() =>
            //        {
            //            AttachVideoPreview(itemText, timelineItemContainer);
            //        });

            //        threadRequest.Start();

            //    }

            //}
        }

        public void Remove(TimelineItemContainer timelineItemContainer)
        {
        }

        private void RespCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                object[] args = asynchronousResult.AsyncState as object[];


                HttpWebRequest request = (HttpWebRequest)args[0];
                HttpWebRequest myHttpWebRequest2 = request;
                HttpWebResponse response = (HttpWebResponse)myHttpWebRequest2.EndGetResponse(asynchronousResult);

                AttachVideoPreview(response.ResponseUri.AbsoluteUri.ToString(), args[1] as TimelineItemContainer);

                response.Close();
            }
            catch (WebException e)
            {
                // Need to handle the exception
            }
        }


        private void AttachVideoPreview(string itemText, TimelineItemContainer item)
        {

            MatchCollection matches = new Regex(YOUTUBE_URIREGEX).Matches(itemText);

            foreach (Match match in matches)
            {
                string vidID = string.Empty;
                vidID = match.Groups[1].Value;

                VideoPreviewAttachment vidAttach = new VideoPreviewAttachment();
                vidAttach.ThumbnailURI = new Uri("http://img.youtube.com/vi/" + vidID + "/0.jpg");
                vidAttach.VideoURI = new Uri(match.Value.ToUpperInvariant().StartsWith(@"HTTP://") ? match.Value : @"http://" + match.Value);
                vidAttach.VideoID = vidID;
                vidAttach.CollapsedText = string.Format("view {0}", match.Value);
                vidAttach.Provider = Provider.YouTube;
                item.AddAttachment(vidAttach);
            }

            //matches = new Regex(YOUTUBESHORT_URIREGEX).Matches(itemText);

            //foreach (Match match in matches)
            //{
            //    string vidID = string.Empty;
            //    vidID = match.Groups[1].Value;

            //    VideoPreviewAttachment vidAttach = new VideoPreviewAttachment();
            //    vidAttach.ThumbnailURI = new Uri("http://img.youtube.com/vi/" + vidID + "/0.jpg");
            //    vidAttach.VideoURI = new Uri(match.Value);
            //    vidAttach.VideoID = vidID;
            //    vidAttach.CollapsedText = string.Format("view {0}", match.Value);
            //    vidAttach.Provider = Provider.YouTube;
            //    item.AddAttachment(vidAttach);
            //}

            matches = new Regex(TWITVID_URIREGEX).Matches(itemText);

            foreach (Match match in matches)
            {
                string vidID = string.Empty;
                vidID = match.Groups[1].Value;

                VideoPreviewAttachment vidAttach = new VideoPreviewAttachment();
                vidAttach.ThumbnailURI = new Uri("http://www.twitvid.com/" + vidID + ":thumb");
                vidAttach.VideoURI = new Uri(match.Value.ToUpperInvariant().StartsWith(@"HTTP://") ? match.Value : @"http://" + match.Value);
                vidAttach.VideoID = vidID;
                vidAttach.CollapsedText = string.Format("view {0}", match.Value);
                vidAttach.Provider = Provider.TwitVid;
                item.AddAttachment(vidAttach);
            }

            matches = new Regex(VIMEO_URIREGEX).Matches(itemText);

            foreach (Match match in matches)
            {
                string vidID = string.Empty;
                vidID = match.Groups[1].Value;

                VideoPreviewAttachment vidAttach = new VideoPreviewAttachment();
                vidAttach.GetThumbnailURI(Provider.Vimeo, vidID);
                vidAttach.VideoURI = new Uri(match.Value.ToUpperInvariant().StartsWith(@"HTTP://") ? match.Value : @"http://" + match.Value);
                vidAttach.VideoID = vidID;
                vidAttach.CollapsedText = string.Format("view {0}", match.Value);
                vidAttach.Provider = Provider.Vimeo;
                item.AddAttachment(vidAttach);
            }
        }


        ///// <summary>
        ///// Called when [expand bitly URL completed].
        ///// </summary>
        ///// <param name="sender">The sender.</param>
        ///// <param name="args">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing the event data.</param>
        //private void OnExpandBitlyURLCompleted(object sender, OpenReadCompletedEventArgs args)
        //{
        //    if (args.Error == null)
        //    {
        //        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(BitlyResponse));
        //        BitlyResponse response = serializer.ReadObject(args.Result) as BitlyResponse;
        //        if (response != null)
        //        {
        //            URL url = Enumerable.FirstOrDefault<URL>(response.Data.Long_URLs);
        //            AttachVideoPreview(url.Long_URL, args.UserState as TimelineItemContainer);
        //        }
        //    }
        //}

        ///// <summary>
        ///// Called when [expand UnShortMe completed].
        ///// </summary>
        ///// <param name="sender">The sender.</param>
        ///// <param name="args">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing the event data.</param>
        //private void OnExpandUnShortMeURLCompleted(object sender, OpenReadCompletedEventArgs args)
        //{
        //    if (args.Error == null)
        //    {
        //        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(UnShortmeResponse));
        //        UnShortmeResponse response = serializer.ReadObject(args.Result) as UnShortmeResponse;
        //        if (response != null && response.Success)
        //        {
        //            AttachVideoPreview(response.ResolvedURL, args.UserState as TimelineItemContainer);
        //        }
        //    }
        //}

        ///// <summary>
        ///// Called when [expand migre me URL completed].
        ///// </summary>
        ///// <param name="sender">The sender.</param>
        ///// <param name="args">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing the event data.</param>
        //private void OnExpandMigreMeURLCompleted(object sender, OpenReadCompletedEventArgs args)
        //{
        //    if (args.Error == null)
        //    {
        //        XmlReader reader = null;
        //        try
        //        {
        //            string xmlResponse = (new StreamReader(args.Result)).ReadToEnd();

        //            // To make this a little more compatible on systems that don't support iso-8859-1, switch to unicode
        //            reader = XmlReader.Create(new StringReader(xmlResponse.Replace("iso-8859-1", "utf-8")));

        //            while (reader.Read())
        //            {
        //                if (reader.NodeType == XmlNodeType.Element)
        //                {
        //                    if (reader.Name.Equals("url", StringComparison.InvariantCultureIgnoreCase))
        //                    {
        //                        // We found a url value! let's process it
        //                        string url = reader.ReadElementContentAsString();
        //                        AttachVideoPreview(url, args.UserState as TimelineItemContainer);
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Yeah, I hate this but let's do it
        //            return;
        //        }
        //    }
        //}


    }
}