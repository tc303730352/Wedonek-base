namespace Base.FileRemoteModel.Down.Model
{
    public class HttpRequestArg
    {
        public string Accept
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
        public TimeSpan? Timeout
        {
            get;
            set;
        }

        public Uri Referer
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

    }
}
