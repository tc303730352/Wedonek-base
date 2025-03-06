using Base.FileModel.UserFileDir;
using Base.FileService.Interface;

namespace Base.FileService.Model
{
    public class UpArg
    {
        public long UserId { get; set; }

        public long? LinkBizPk { get; set; }

        public UserFileDirDto UserDir { get; set; }

        public IDirState Dir { get; set; }
        public string Tag { get; set; }

        public string UserType { get; set; }
    }
}
