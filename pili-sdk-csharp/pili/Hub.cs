using StreamList = pili_sdk_csharp.pili.Stream.StreamList;
using Credentials = pili_sdk_csharp.pili_qiniu.Credentials;
using MessageConfig = pili_sdk_csharp.pili_common.MessageConfig;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace pili_sdk_csharp.pili
{

    public class Hub
    {

        private Credentials mCredentials;
        private string mHubName;
        public Hub(Credentials credentials, string hubName)
        {
            if (hubName == null)
            {
                throw new System.ArgumentException(MessageConfig.NULL_HUBNAME_EXCEPTION_MSG);
            }
            if (credentials == null)
            {
                throw new System.ArgumentException(MessageConfig.NULL_CREDENTIALS_EXCEPTION_MSG);
            }
            mCredentials = credentials;
            mHubName = hubName;
        }

        public virtual bool createStream(string title)
        {
            return API.createStream(mCredentials, mHubName, title);
        }

        public virtual Stream getStream(string streamTitle)
        {
            return API.getStream(mCredentials, mHubName, streamTitle);
        }

        //Credentials credentials, string hubName, bool liveonly, string titlePrefix, long limitCount, string startMarker
        public virtual JObject listStreams()
        {
            return API.listStreams(mCredentials, mHubName, false, null, 1000,null);
        }

        public virtual JObject listStreams(long limit,string marker)
        {
            return API.listStreams(mCredentials, mHubName, false, null,limit ,marker);
        }
        public virtual JObject listStreams(string titlePrefix,long limit, string marker)
        {
            return API.listStreams(mCredentials, mHubName,false, titlePrefix, limit, marker);
        }
        public virtual JObject listStreams(bool liveonly,string titlePrefix, long limit, string marker)
        {
            return API.listStreams(mCredentials, mHubName, liveonly, titlePrefix, limit, marker);
        }
        //(Credentials credentials, string hubName, string streamtitle,int disabledTil
        public virtual void disableStream(string streamtitle,int disabledTill)
        {
            API.disableStream(mCredentials, mHubName, streamtitle, disabledTill);
        }
        public virtual JObject getStreamLiveInformation(string streamTitle)
        {
            return API.getStreamLiveInformation(mCredentials, mHubName, streamTitle);
        }
        public virtual JObject getStreamsLiveInformation(string[] streams)
        { 
        
            return API.getStreamsLiveInformation(mCredentials, mHubName, streams);
        }
        public virtual JObject getStreamHistory(string stream,int start,int end)
        {
            return API.getStreamHistory(mCredentials,mHubName,stream,start, end);
        }
        public virtual JObject Saveas(string stream,string fname,int start,int end,string format,string pipeline,string notify,int expireDays)
        {
            return API.saveAs(mCredentials, mHubName, stream, fname, start, end, format, pipeline, notify, expireDays);
        }
        public virtual JObject snapshot(string stream, string fileName, int time, string format, int deleteAfterDays)
        {
            return API.snapshot(mCredentials, mHubName, stream, fileName, time, format, deleteAfterDays);
        }
        public virtual JObject profileConvert(string stream,string[]  profileNames)
        {
            return API.profileconvert(mCredentials, mHubName, stream, profileNames);
        }
    }
}