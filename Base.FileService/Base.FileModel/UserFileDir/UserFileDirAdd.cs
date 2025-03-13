using WeDonekRpc.Helper.Validate;
namespace Base.FileModel.UserFileDir
{
    public class UserFileDirAdd : UserFileDirSet
    {
        /// <summary>
        /// 目录Key
        /// </summary>
        [NullValidate("file.user.dir.key.null")]
        [LenValidate("file.user.dir.key.len", 2, 50)]
        [FormatValidate("file.user.dir.key.error", ValidateFormat.数字字母)]
        public string DirKey { get; set; }

    }
}
