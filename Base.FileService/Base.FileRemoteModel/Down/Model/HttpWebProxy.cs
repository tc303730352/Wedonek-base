namespace Base.FileRemoteModel.Down.Model
{
    public class HttpWebProxy
    {
        public string Address
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
        public string UserName
        {
            get;
            set;
        }
        public string Pwd
        {
            get;
            set;
        }
        public string Domain
        {
            get;
            set;
        }
    }
}
