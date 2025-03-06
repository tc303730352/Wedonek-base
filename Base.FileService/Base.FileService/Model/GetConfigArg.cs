using WeDonekRpc.Helper.Validate;

namespace Base.FileService.Model
{
    public class GetConfigArg
    {
        [NullValidate("file.user.dir.key.null")]
        [LenValidate("file.user.dir.key.len", 2, 50)]
        public string DirKey
        {
            get;
            set;
        }
        public long? LinkBizPk
        {
            get;
            set;
        }
        [LenValidate("file.tag.len", 0, 50)]
        public string Tag
        {
            get;
            set;
        }
    }
}
