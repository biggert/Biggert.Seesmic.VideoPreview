using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private const string YOUTUBE_URIREGEX = @"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)";
        private const string YOUTUBE_URI = @"http://www.youtube.com/watch?v=Lxp_3000h_U";
        private const string YOUTUBESHORT_URIREGEX = "http://youtu.be/(?<VideoID>[0-9a-zA-Z\\-_]*)";
        private const string YOUTUBESHORT_URI = @"http://youtu.be/oHg5SJYRHA0";
        private const string TWITVIDWWW_URIREGEX = "http://www.twitvid.com/(?<VideoID>[0-9a-zA-Z]*)";
        private const string TWITVIDWWW_URI = @"http://www.twitvid.com/9OORM";
        private const string TWITVID_URIREGEX = "http://twitvid.com/(?<VideoID>[0-9a-zA-Z]*)";
        private const string TWITVID_URI = @"http://twitvid.com/9OORM";

        private const string URIREGEX = @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$";

        [TestMethod]
        public void YouTube_URIRegEx_Test()
        {

            MatchCollection matchs = new Regex(YOUTUBE_URIREGEX).Matches(YOUTUBE_URI);
            Assert.IsTrue(matchs.Count > 0);

            //foreach (Match match in matchs)
            //{

            //    Assert.Inconclusive();
            //}
        }

        [TestMethod]
        public void YouTubeShort_URIRegEx_Test()
        {

            MatchCollection matchs = new Regex(YOUTUBESHORT_URIREGEX).Matches(YOUTUBESHORT_URI);
            Assert.IsTrue(matchs.Count > 0);
        }
        [TestMethod]
        public void TwitvidWWW_URIRegEx_Test()
        {

            MatchCollection matchs = new Regex(TWITVIDWWW_URIREGEX).Matches(TWITVIDWWW_URI);
            Assert.IsTrue(matchs.Count > 0);
        }
        [TestMethod]
        public void Twitvid_URIRegEx_Test()
        {

            MatchCollection matchs = new Regex(TWITVID_URIREGEX).Matches(TWITVID_URI);
            Assert.IsTrue(matchs.Count > 0);
        }


        [TestMethod]
        public void YouTube_VidID_Test()
        {

            MatchCollection matchs = new Regex(YOUTUBE_URIREGEX).Matches(YOUTUBE_URI);

            foreach (Match match in matchs)
            {
                string vidID = string.Empty;
                vidID = match.Groups["VideoID"].Value;

                Assert.IsFalse(string.IsNullOrEmpty(vidID));
                Assert.AreEqual("oHg5SJYRHA0", vidID);

            }
        }
        [TestMethod]
        public void YouTubeshort_VidID_Test()
        {

            MatchCollection matchs = new Regex(YOUTUBESHORT_URIREGEX, RegexOptions.IgnoreCase).Matches(YOUTUBESHORT_URI);

            foreach (Match match in matchs)
            {
                string vidID = string.Empty;
                vidID = match.Groups["VideoID"].Value;
                Assert.IsFalse(string.IsNullOrEmpty(vidID));
                Assert.AreEqual("oHg5SJYRHA0", vidID);
            }
        }
        [TestMethod]
        public void TwitVidWWW_VidID_Test()
        {

            MatchCollection matchs = new Regex(TWITVIDWWW_URIREGEX).Matches(TWITVIDWWW_URI);

            foreach (Match match in matchs)
            {
                string vidID = string.Empty;
                vidID = match.Groups["VideoID"].Value;
                Assert.IsFalse(string.IsNullOrEmpty(vidID));
                Assert.AreEqual("9OORM", vidID);
            }
        }
        [TestMethod]
        public void TwitVid_VidID_Test()
        {

            MatchCollection matchs = new Regex(TWITVID_URIREGEX).Matches(TWITVID_URI);

            foreach (Match match in matchs)
            {
                string vidID = string.Empty;
                vidID = match.Groups["VideoID"].Value;
                Assert.IsFalse(string.IsNullOrEmpty(vidID));
                Assert.AreEqual("9OORM", vidID);
            }
        }

        [TestMethod]
        public void URI_Test()
        {
            MatchCollection matchs = new Regex(@"[\w-_]+(\.[\w-_]+)+([\w-.,@?^=%&:/~+#]*[\w-\@?^=%&/~+#])?", RegexOptions.Compiled | RegexOptions.IgnoreCase).Matches("hello http://www.google.com http://youtu.be youtu.be/test");

            int test = matchs.Count;

            foreach (Match match in matchs)
            {

            }
        }

        [TestMethod]
        public void WebClientShortURI_Test()
        {
            MatchCollection matchs = new Regex(@"[\w-_]+(\.[\w-_]+)+([\w-.,@?^=%&:/~+#]*[\w-\@?^=%&/~+#])?", RegexOptions.Compiled | RegexOptions.IgnoreCase).Matches("http://t.co/QPgSFpk");

            int test = matchs.Count;

            foreach (Match match in matchs)
            {
                string url = match.Value;
                string requestURL = @"http://" + url.Trim();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestURL);
                
                // Start the asynchronous request.
                IAsyncResult result =
                  (IAsyncResult)request.BeginGetResponse(new AsyncCallback(RespCallback), request);
            }

            while (true)
            {
                Thread.Sleep(5000);
            }

        }

        [TestMethod]
        public void WebClientTwitVidShortURI_Test()
        {
            MatchCollection matchs = new Regex(@"[\w-_]+(\.[\w-_]+)+([\w-.,@?^=%&:/~+#]*[\w-\@?^=%&/~+#])?", RegexOptions.Compiled | RegexOptions.IgnoreCase).Matches("http://goo.gl/3ruKg");

            int test = matchs.Count;

            foreach (Match match in matchs)
            {
                string url = match.Value;
                string requestURL = @"http://" + url.Trim();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestURL);

                // Start the asynchronous request.
                IAsyncResult result =
                  (IAsyncResult)request.BeginGetResponse(new AsyncCallback(RespCallback), request);
            }

            while (true)
            {
                Thread.Sleep(5000);
            }

        }


        [TestMethod]
        public void WebClientVimeoShortURI_Test()
        {
            MatchCollection matchs = new Regex(@"[\w-_]+(\.[\w-_]+)+([\w-.,@?^=%&:/~+#]*[\w-\@?^=%&/~+#])?", RegexOptions.Compiled | RegexOptions.IgnoreCase).Matches("http://goo.gl/qfHqs");

            int test = matchs.Count;

            foreach (Match match in matchs)
            {
                string url = match.Value;
                string requestURL = @"http://" + url.Trim();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestURL);

                // Start the asynchronous request.
                IAsyncResult result=
                  (IAsyncResult) request.BeginGetResponse(new AsyncCallback(RespCallback), request);
            }

            while (true)
            {
                Thread.Sleep(5000);
            }
        }

        private static void RespCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                // State of request is asynchronous.
                HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
                HttpWebRequest myHttpWebRequest2 = request;
                HttpWebResponse response = (HttpWebResponse)myHttpWebRequest2.EndGetResponse(asynchronousResult);

                Console.WriteLine(response.ResponseUri.AbsoluteUri.ToString());

                response.Close();
            }
            catch (WebException e)
            {
                // Need to handle the exception
            }
        }

        void client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            WebClient client = (WebClient)sender;

            Console.Write(client.ResponseHeaders[4].ToString());
        }

    }
}
