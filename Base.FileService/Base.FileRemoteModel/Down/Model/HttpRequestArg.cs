using System.Net;
using WeDonekRpc.Helper.Http;

namespace Base.FileRemoteModel.Down.Model
{
    public class HttpRequestArg
    {
        public string Accept
        {
            get;
            set;
        }
        public SecurityProtocolType? SecurityProtocolType
        {
            get;
            set;
        }
        public string RequestEncoding
        {
            get;
            set;
        }
        public string ResponseEncoding
        {
            get;
            set;
        }
        public Dictionary<string, string> Cookies
        {
            get;
            set;
        }
        public Dictionary<string, string> HeadList
        {
            get;
            set;
        }
        public int? Timeout
        {
            get;
            set;
        }
        /// <summary>
        /// 读写超时时间
        /// </summary>
        public int? ReadWriteTimeout
        {
            get;
            set;
        }
        public string Referer
        {
            get;
            set;
        }
        public string UserAgent
        {
            get;
            set;
        }

        public string ContentType
        {
            get;
            set;
        }

        public CertInfo HttpsCert
        {
            get;
            set;
        }

        public int? ContinueTimeout { get; set; }
        public string ProtocolVersion { get; set; }
        public bool? SendChunked { get; set; }
    }
}
