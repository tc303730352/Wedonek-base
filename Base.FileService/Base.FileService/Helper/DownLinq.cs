using System.Text;
using Base.FileRemoteModel.Down.Model;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.Http;

namespace Base.FileService.Helper
{
    internal static class DownLinq
    {
        private static readonly RequestSet _defRequestSet = new RequestSet();

        private static string _GetContentType ( RequestDataType dataType, string extension )
        {
            if ( dataType == RequestDataType.None )
            {
                if ( extension.IsNull() )
                {
                    return "application/octet-stream";
                }
                return HttpHeaderHelper.GetContentType(extension);
            }
            else if ( dataType == RequestDataType.JSON )
            {
                return "application/json";
            }
            else if ( dataType == RequestDataType.XML )
            {
                return "text/xml";
            }
            else if ( dataType == RequestDataType.Form )
            {
                return "application/x-www-form-urlencoded";
            }
            else if ( dataType == RequestDataType.Text )
            {
                return "text/plain";
            }
            return "application/octet-stream";
        }
        public static HttpRequestMessage ToHttpRequestMessage ( this DownFileParam param, RequestSet set, Uri uri )
        {
            HttpMethod method = HttpMethod.Parse(param.HttpMethod);
            if ( method != HttpMethod.Post || method != HttpMethod.Get || method != HttpMethod.Put || method != HttpMethod.Delete )
            {
                throw new ErrorException("file.http.method.not.supported");
            }
            HttpRequestMessage message = new HttpRequestMessage(method, uri);
            if ( message.Method == HttpMethod.Put || message.Method == HttpMethod.Post )
            {
                string contentType = _GetContentType(param.DataType, param.Extension);
                message.Content = new StringContent(param.PostData, set.RequestEncoding, contentType);
            }
            return message;
        }
        public static RequestSet ToRequestSet ( this HttpRequestArg arg )
        {
            if ( arg == null )
            {
                return _defRequestSet;
            }
            RequestSet req = new RequestSet();
            req.Referer = arg.Referer;
            req.HeadList = arg.HeadList;
            req.Cookies = arg.Cookies;
            if ( arg.Accept.IsNotNull() )
            {
                req.Accept = arg.Accept;
            }
            if ( arg.RequestEncoding.IsNotNull() )
            {
                req.RequestEncoding = Encoding.GetEncoding(arg.RequestEncoding);
            }
            if ( arg.ResponseEncoding.IsNotNull() )
            {
                req.ResponseEncoding = Encoding.GetEncoding(arg.RequestEncoding);
            }
            if ( arg.Timeout.HasValue )
            {
                req.Timeout = arg.Timeout.Value;
            }
            if ( arg.UserAgent.IsNotNull() )
            {
                req.UserAgent = arg.UserAgent;
            }
            return req;
        }
    }
}
