namespace Base.FileRemoteModel.Down.Model
{
    public class HttpWebProxy
    {
        public Uri Address
        {
            get;
            set;
        }

        public bool BypassOnLocal
        {
            get;
            set;
        }
        public string[] BypassList
        {
            get;
            set;
        }
        public Credentials Credentials
        {
            get;
            set;
        }
    }
    public class Credentials
    {
        /// <summary>
        /// 方案名
        /// </summary>
        public string Scheme
        {
            get;
            set;
        }
        public Uri Uri
        {
            get;
            set;
        }
        public string AuthType
        {
            get;
            set;
        }
    }
}
