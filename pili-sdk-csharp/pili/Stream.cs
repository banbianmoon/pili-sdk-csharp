using System;
using System.Collections.Generic;
using Newtonsoft;
using Credentials = pili_sdk_csharp.pili_qiniu.Credentials;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace pili_sdk_csharp.pili
{


    public class Stream
    {
        public const string ORIGIN = "ORIGIN";
        private string mStreamJsonStr;
        private Credentials mCredentials;
        private string createdAt; // Time ISO 8601
        private string updatedAt; // Time ISO 8601
        private string expireAt;
        private int disabledTill;
        private string[] profiles = null;
        private bool watermark;


        public Stream(JObject jsonObj)
        {
            createdAt = jsonObj["createdAt"].ToString();
            updatedAt = jsonObj["updatedAt"].ToString();
            expireAt = jsonObj["expireAt"].ToString();
            disabledTill = (int)jsonObj["disabledTill"];
            watermark = (bool)jsonObj["watermark"];

            if (jsonObj["converts"] != null)
            {
                profiles = JsonConvert.DeserializeAnonymousType(jsonObj["converts"].ToString(), profiles);
            }
            mStreamJsonStr = jsonObj.ToString();
        }
        public string toJsonString()
        {
            return mStreamJsonStr;
        }
        public Stream(JObject jsonObject, Credentials credentials)
            : this(jsonObject)
        {
            mCredentials = credentials;
        }
        public virtual string[] Profiles
        {
            get
            {
                return profiles;
            }
        }
        public virtual bool Watermark
        {
            get
            {
                return watermark;
            }
        }
        public virtual string CreatedAt
        {
            get
            {
                return createdAt;
            }
        }
        public virtual string UpdatedAt
        {
            get
            {
                return updatedAt;
            }
        }
        public virtual string ExpireAt
        {
            get
            {
                return expireAt;
            }
        }
        public virtual int DisabledTill
        {
            get
            {
                return disabledTill;
            }
        }

        public class Segment
        {
            private long start;
            private long end;

            public Segment(long start, long end)
            {
                this.start = start;
                this.end = end;
            }
            public virtual long Start
            {
                get
                {
                    return start;
                }
            }
            public virtual long End
            {
                get
                {
                    return end;
                }
            }
        }


        public class SaveAsResponse
        {
            private string url;
            private string targetUrl;
            private string persistentId;
            private string mJsonString;

            public SaveAsResponse(JObject jsonObj)
            {
                url = jsonObj["url"].ToString();
                try
                {
                    targetUrl = jsonObj["targetUrl"].ToString();
                    //
                }
                catch (System.NullReferenceException)
                {
                    // do nothing. ignore.
                }
                persistentId = jsonObj["persistentId"].ToString();
                mJsonString = jsonObj.ToString();
            }

            public virtual string Url
            {
                get
                {
                    return url;
                }
            }
            public virtual string TargetUrl
            {
                get
                {
                    return targetUrl;
                }
            }
            public virtual string PersistentId
            {
                get
                {
                    return persistentId;
                }
            }

            public override string ToString()
            {
                return mJsonString;
            }
        }

        public class SnapshotResponse
        {
            private string targetUrl;
            private string persistentId;
            private string mJsonString;
            public SnapshotResponse(JObject jsonObj)
            {
                targetUrl = jsonObj["targetUrl"].ToString();
                persistentId = jsonObj.GetValue("persistentId") == null ? null : jsonObj["persistentId"].ToString();
                mJsonString = jsonObj.ToString();
            }

            public virtual string TargetUrl
            {
                get
                {
                    return targetUrl;
                }
            }
            public virtual string PersistentId
            {
                get
                {
                    return persistentId;
                }
            }

            public override string ToString()
            {
                return mJsonString;
            }
        }

        public class FramesPerSecond
        {
            private float audio;
            private float video;
            private float data;
            public FramesPerSecond(float audio, float video, float data)
            {
                this.audio = audio;
                this.video = video;
                this.data = data;
            }

            public virtual float Audio
            {
                get
                {
                    return audio;
                }
            }
            public virtual float Video
            {
                get
                {
                    return video;
                }
            }
            public virtual float Data
            {
                get
                {
                    return data;
                }
            }
        }

        public class SegmentList
        {
            private IList<Segment> segmentList;



            public SegmentList(JObject jsonObj)
            {
                segmentList = new List<Segment>();
                JArray jlist = JArray.Parse(jsonObj["segments"].ToString());
                for (int i = 0; i < jlist.Count; ++i)
                {
                    JObject tempo = JObject.Parse(jlist[i].ToString());
                    segmentList.Add(new Segment((long)tempo["start"], (long)tempo["end"]));
                }


            }

            public virtual IList<Segment> getSegmentList()
            {
                return segmentList;
            }
        }

        public class Status
        {
            private string addr;
            private string status;
            private float bytesPerSecond;
            private FramesPerSecond framesPerSecond;
            private string startFrom;
            private string mJsonString;
            public Status(JObject jsonObj)
            {

                addr = jsonObj["addr"].ToString();
                status = jsonObj["status"].ToString();
                DateTime startFrominit = (DateTime)jsonObj["startFrom"];
                startFrom = startFrominit.ToString("yyyy-MM-ddTHH:mm:ssZ");
                try
                {
                    bytesPerSecond = (float)jsonObj["bytesPerSecond"];
                    float audio = (float)jsonObj["framesPerSecond"]["audio"];
                    float video = (float)jsonObj["framesPerSecond"]["video"];
                    float data = (float)jsonObj["framesPerSecond"]["data"];
                    framesPerSecond = new FramesPerSecond(audio, video, data);
                }
                catch (System.NullReferenceException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
                mJsonString = jsonObj.ToString();
            }
            public virtual string Addr
            {
                get
                {
                    return addr;
                }
            }
            public virtual string StartFrom
            {
                get
                {
                    return startFrom;
                }

            }
            public virtual string getStatus()
            {
                return status;
            }
            public virtual float BytesPerSecond
            {
                get
                {
                    return bytesPerSecond;
                }
            }
            public virtual FramesPerSecond FramesPerSecond
            {
                get
                {
                    return framesPerSecond;
                }
            }

            public override string ToString()
            {
                return mJsonString;
            }
        }

        public class StreamList
        {
            private string marker;
            private IList<String> itemList;
            public StreamList(JObject jsonObj, Credentials auth)
            {
                Console.WriteLine(jsonObj.ToString());
                this.marker = jsonObj["marker"].ToString();
                Console.WriteLine("this.marker-----" + this.marker);

                try
                {
                    JToken record = jsonObj["items"];
                    itemList = new List<String>();
                    foreach (JObject jp in record)
                    {
                        itemList.Add(jp.ToString());
                    }

                }
                catch (System.InvalidCastException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
            }

            public virtual string Marker
            {
                get
                {
                    return marker;
                }
            }
            public virtual IList<String> Streams
            {
                get
                {
                    return itemList;
                }
            }
        }

        /*
        public virtual Stream update(string publishKey, string publishSecrity, bool disabled)
        {
           // return API.updateStream(mCredentials, this.id, publishKey, publishSecrity, disabled);
        }


        public virtual SegmentList segments()
        {
           // return API.getStreamSegments(mCredentials, this.id, 0, 0, 0);
        }


        public virtual SegmentList segments(long start, long end)
        {
           // return API.getStreamSegments(mCredentials, this.id, start, end, 0);
        }


        public virtual SegmentList segments(long start, long end, int limit)
        {
            //return API.getStreamSegments(mCredentials, this.id, start, end, limit);
        }


        public virtual Status status()
        {
           // return API.getStreamStatus(mCredentials, this.id);
        }


        public virtual string rtmpPublishUrl()
        {
            return API.publishUrl(this, 0);
        }
        public virtual IDictionary<string, string> rtmpLiveUrls()
        {
            return API.rtmpLiveUrl(this);
        }
        public virtual IDictionary<string, string> hlsLiveUrls()
        {
            return API.hlsLiveUrl(this);
        }

        public virtual IDictionary<string, string> hlsPlaybackUrls(long start, long end)
        {
            return API.hlsPlaybackUrl(this.mCredentials, this.id, start, end);
        }
        
        public virtual IDictionary<string, string> httpFlvLiveUrls()
        {
            return API.httpFlvLiveUrl(this);
        }

  
        public virtual string delete()
        {
           // return API.deleteStream(mCredentials, this.id);
        }

        public virtual string toJsonString()
        {
            return mStreamJsonStr;
        }


        public virtual SaveAsResponse saveAs(string fileName, string format, long startTime, long endTime, string notifyUrl,string pipleline)
        {
           // return API.saveAs(mCredentials, this.id, fileName, format, startTime, endTime, notifyUrl,pipleline);
        }

        public virtual SaveAsResponse saveAs(string fileName, string format, long startTime, long endTime)
        {
            return saveAs(fileName, format, startTime, endTime, null,null);
        }

        public virtual SaveAsResponse saveAs(string fileName, string format, string notifyUrl,string pipleline)
        {
            return saveAs(fileName, format, 0, 0, notifyUrl,pipleline);
        }
        public virtual SaveAsResponse saveAs(string fileName, string format)
        {
            return saveAs(fileName, format, 0, 0, null,null);
        }   

public virtual SnapshotResponse snapshot(string name, string format)
{
    return API.snapshot(mCredentials, this.id, name, format, 0, null);
}

public virtual SnapshotResponse snapshot(string name, string format, string notifyUrl)
{
    return API.snapshot(mCredentials, this.id, name, format, 0, notifyUrl);
}

public virtual SnapshotResponse snapshot(string name, string format, long time, string notifyUrl)
{
    return API.snapshot(mCredentials, this.id, name, format, time, notifyUrl);
}


public virtual Stream enable()
{
    return API.updateStream(mCredentials, this.id, null, null, false);
}

public virtual Stream disable()
{
    return API.updateStream(mCredentials, this.id, null, null, true);
}
*/
    }
}