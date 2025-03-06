using WeDonekRpc.Helper.Validate;
namespace Base.FileService.Model
{
    public class PreUpFile
    {
        /// <summary>
        /// 文件MD5
        /// </summary>
        [NullValidate("file.md5.null")]
        [LenValidate("file.md5.len", 32)]
        [FormatValidate("file.md5.error", ValidateFormat.数字字母)]
        public string FileMd5
        {
            get;
            set;
        }
        [NullValidate("file.name.null")]
        [LenValidate("file.name.len", 1, 50)]
        [RegexValidate("file.name.error", "^.+[.]\\w+$")]
        public string FileName { get; set; }

        public long? LinkBizPk
        {
            get;
            set;
        }
        public string Tag
        {
            get;
            set;
        }
    }
}
