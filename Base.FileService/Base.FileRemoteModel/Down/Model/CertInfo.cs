using System.Security.Cryptography.X509Certificates;

namespace Base.FileRemoteModel.Down.Model
{
    public class CertInfo
    {
        public byte[] CertFile
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public X509KeyStorageFlags KeyStorageFlags
        {
            get;
            set;
        }
    }
}
