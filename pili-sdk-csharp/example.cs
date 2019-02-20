using pili_sdk_csharp.pili;
using pili_sdk_csharp.pili_qiniu;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreamList = pili_sdk_csharp.pili.Stream.StreamList;
using Newtonsoft.Json.Linq;

namespace pili_sdk_csharp
{
    class example
    {
        private const string ACCESS_KEY = "5vxiMsV_f6ok2srxn92XONQIbaspEgfHth4JHOac";
        private const string SECRET_KEY = "uyLzMBEGr_Du4u379g5mY5E9ncZiRTjhfotGKwm_";

        private const string HUB_NAME = "llj-hub";

        static void Main(string[] args)
        {
            //testCreatStream(); 
            //testgetStream();
            //testlistStream();
            //testdisableStream();
            //testgetStreamLiveInformation();
            //testgetStreamsLiveInformation();
            //testStreamHistory();

            //RTMP推流直播地址
            Credentials cred = new Credentials(ACCESS_KEY,SECRET_KEY);
            string streamTitle = "test01";
            int expireat = 1579592233;
            string domain = "pili-publish.lilanjun-test.qiniuts.com";
            string rtmpPublishUrl = Credentials.signUrl(ACCESS_KEY, SECRET_KEY,domain,HUB_NAME,streamTitle,expireat);
            Console.WriteLine("RTMP推流直播地址" + rtmpPublishUrl);
 
            //RTMP播放地址
            string rtmpPlayerUrl = "rtmp://xxxxx/" + HUB_NAME + "/" + streamTitle;

            //hls播放地址
            string hlsPlayerUrl = "http://xxxxx/" + HUB_NAME + "/" + streamTitle + ".m3u8";

            //HDL播放地址
            string hdlPlayerUrl = "http://xxxxx/" + HUB_NAME + "/" + streamTitle + ".flv";

            //直播封面地址
            string  snapshotUrl = "http://xxxxx/" + HUB_NAME + "/" + streamTitle + ".jpg";

            testSaveas();
            //testsnapshot();
            //testProfileConvert();
        }
     
        public static void testCreatStream()
        {
            Credentials credentials = new Credentials(ACCESS_KEY, SECRET_KEY); // Credentials Object
            Hub hub = new Hub(credentials, HUB_NAME);

            // Create a new Stream
            string title = "test01"; // optional, auto-generated as default
            try
            {
                if(hub.createStream(title))
                {
                    Trace.WriteLine("hub.createStream:"+title);
                }
            }
            catch (PiliException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }        
        }
        public static void testgetStream()
        {
            try
            {
                Credentials credentials = new Credentials(ACCESS_KEY, SECRET_KEY); // Credentials Object
                Hub hub = new Hub(credentials, HUB_NAME);
                string streamTitle = "";
                Stream stream = hub.getStream(streamTitle);
                Console.WriteLine("hub.getStream:"+streamTitle);
                Console.WriteLine(stream.toJsonString());
            }
            catch (PiliException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
        }
        public static void testlistStream()
        {
            try
            {
                Credentials credentials = new Credentials(ACCESS_KEY, SECRET_KEY); // Credentials Object
                Hub hub = new Hub(credentials, HUB_NAME);
                // bool liveonly, string titlePrefix, long limitCount, string startMarker
                bool liveonly = false;
                string titlePrefix = "";
                long limitCount = 20;
                string marker = "";
                JObject result = hub.listStreams(liveonly,titlePrefix,limitCount,marker);
                Console.WriteLine("hub.listStream:" + HUB_NAME);
                Console.WriteLine("marker="+result["marker"]);
                JToken streams = result["items"];
                Console.WriteLine("items:");
                foreach(JObject stream in streams)
                {
                    Console.WriteLine(stream.ToString());
                }
            }
            catch (PiliException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
        }
        public static void testdisableStream()
        {
            Credentials credentials = new Credentials(ACCESS_KEY, SECRET_KEY); // Credentials Object
            Hub hub = new Hub(credentials, HUB_NAME);
            string streamTitle = "";
            int disabledTill = 1;
            hub.disableStream(streamTitle, disabledTill);
            Console.WriteLine(streamTitle+" 禁播: " +disabledTill);
        }
        public static void testgetStreamLiveInformation()
        {
            Credentials credentials = new Credentials(ACCESS_KEY, SECRET_KEY); // Credentials Object
            Hub hub = new Hub(credentials, HUB_NAME);
            string streamTitle = "";
           JObject resultobject =  hub.getStreamLiveInformation(streamTitle);
            Console.WriteLine(resultobject.ToString());
        }
        public static void testgetStreamsLiveInformation()
        {
            Credentials credentials = new Credentials(ACCESS_KEY, SECRET_KEY); // Credentials Object
            Hub hub = new Hub(credentials, HUB_NAME);
            string[]  streams =new string[]{ "" };
            JObject resultobject = hub.getStreamsLiveInformation(streams);
            Console.WriteLine(resultobject.ToString());
        }
        public static void testStreamHistory()
        {
            Credentials credentials = new Credentials(ACCESS_KEY, SECRET_KEY); // Credentials Object
            Hub hub = new Hub(credentials, HUB_NAME);
            string stream = "";
            int start = 0;
            int end = 1545131328;
            JObject result = hub.getStreamHistory(stream,start,end);
            Console.WriteLine(result.ToString());
        }
        public static void testSaveas()
        {
            Credentials credentials = new Credentials(ACCESS_KEY, SECRET_KEY); // Credentials Object
            Hub hub = new Hub(credentials, HUB_NAME);
            string stream = "test01";
            string fname = "sjkadjkashkd";
            int start = 1548233940;
            int end = 1548234300;
            string format = "mp4";
            string pipeline = "qiniu";
            string notify = "";
            int expireDays = -1;
            JObject result = hub.Saveas(stream, fname, start, end, format, pipeline, notify, expireDays);
            Console.WriteLine(result.ToString());
        }
        public static void testsnapshot()
        {
            Credentials credentials = new Credentials(ACCESS_KEY, SECRET_KEY); // Credentials Object
            Hub hub = new Hub(credentials, HUB_NAME);
            string stream = "";
            string fname = "";
            int time = 0;
            string format = "";
            int deleteAfterDays = 0;
            JObject result = hub.snapshot(stream,fname,time,format,deleteAfterDays);
            Console.WriteLine(result.ToString());
        }
        public static void testProfileConvert()
        {
            Credentials credentials = new Credentials(ACCESS_KEY, SECRET_KEY); // Credentials Object
            Hub hub = new Hub(credentials, HUB_NAME);
            string stream = "";
            string[] profileNames = new string[] {"480p"};
            JObject result = hub.profileConvert(stream,profileNames);
            Console.WriteLine(result.ToString());
        }
    }
}
