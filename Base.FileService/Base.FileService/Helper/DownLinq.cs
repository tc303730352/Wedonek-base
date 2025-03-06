using System.Text;
using Base.FileRemoteModel.Down.Model;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.Http;

namespace Base.FileService.Helper
{
    internal static class DownLinq
    {
        private static readonly HttpRequestSet _defRequestSet = new HttpRequestSet(HttpReqType.File);
        public static HttpRequestSet ToRequestSet (this HttpRequestArg arg)
        {
            if (arg == null)
            {
                return _defRequestSet;
            }
            HttpRequestSet req = new HttpRequestSet(HttpReqType.File);
            if (arg.HttpsCert != null)
            {
                req.HttpsCert = arg.HttpsCert;
            }
            if (arg.Cookies != null)
            {
                req.Cookies = arg.Cookies;
            }
            if (arg.HeadList != null)
            {
                req.HeadList = arg.HeadList;
            }
            if (arg.Accept.IsNotNull())
            {
                req.Accept = arg.Accept;
            }
            if (arg.ContentType.IsNotNull())
            {
                req.ContentType = arg.ContentType;
            }
            if (arg.ProtocolVersion.IsNotNull())
            {
                req.ProtocolVersion = Version.Parse(arg.ProtocolVersion);
            }
            if (arg.ReadWriteTimeout.HasValue)
            {
                req.ReadWriteTimeout = arg.ReadWriteTimeout.Value;
            }
            if (arg.Referer.IsNotNull())
            {
                req.Referer = arg.Referer;
            }
            if (arg.Referer.IsNotNull())
            {
                req.Referer = arg.Referer;
            }
            if (arg.ContinueTimeout.HasValue)
            {
                req.ContinueTimeout = arg.ContinueTimeout.Value;
            }
            if (arg.RequestEncoding.IsNotNull())
            {
                req.RequestEncoding = Encoding.GetEncoding(arg.RequestEncoding);
            }
            if (arg.ResponseEncoding.IsNotNull())
            {
                req.ResponseEncoding = Encoding.GetEncoding(arg.RequestEncoding);
            }
            if (arg.SecurityProtocolType.HasValue)
            {
                req.SecurityProtocolType = arg.SecurityProtocolType.Value;
            }
            if (arg.SendChunked.HasValue)
            {
                req.SendChunked = arg.SendChunked.Value;
            }
            if (arg.Timeout.HasValue)
            {
                req.Timeout = arg.Timeout.Value;
            }
            if (arg.UserAgent.IsNotNull())
            {
                req.UserAgent = arg.UserAgent;
            }
            return req;
        }
    }
}
