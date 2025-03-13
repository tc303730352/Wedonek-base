using Base.FileRemoteModel;
using WeDonekRpc.Helper.Validate;

namespace Base.FileModel.UserFileDir
{
    public class UserFileDirSet
    {
        /// <summary>
        /// 目录名称
        /// </summary>
        [NullValidate("file.user.dir.name.null")]
        [LenValidate("file.user.dir.name.len", 2, 50)]
        public string DirName { get; set; }

        /// <summary>
        /// 目录状态
        /// </summary>
        [EnumValidate("file.user.dir.status.error", typeof(FileDirStatus))]
        public FileDirStatus DirStatus { get; set; }


        /// <summary>
        /// 允许上传的文件大小
        /// </summary>
        [NumValidate("file.user.dir.allow.up.file.size.error", 1)]
        public long? AllowUpFileSize { get; set; }

        /// <summary>
        /// 允许扩展名
        /// </summary>
        [RegexValidate("file.user.dir.allow.extension.error", @"^[.]\w+$")]
        [LenValidate("file.user.dir.allow.extension.len", 0, 30)]
        public string[] AllowExtension { get; set; }

        /// <summary>
        /// 是否需要登陆
        /// </summary>
        public bool IsAccredit { get; set; }

        /// <summary>
        /// 上传权限码
        /// </summary>
        [FormatValidate("file.user.dir.up.power.error", ValidateFormat.数字字母点)]
        [LenValidate("file.user.dir.up.power.len", 0, 10)]
        public string[] UpPower { get; set; }

        /// <summary>
        /// 访问权限
        /// </summary>
        [EnumValidate("file.user.dir.power.error", typeof(FilePower))]
        public FilePower Power { get; set; }

        /// <summary>
        /// 访问权限码
        /// </summary>
        [FormatValidate("file.user.dir.read.power.error", ValidateFormat.数字字母点)]
        [LenValidate("file.user.dir.read.power.len", 0, 10)]
        public string[] ReadPower { get; set; }
        /// <summary>
        /// 上传图片的设置(上传文件为图片时有效)
        /// </summary>
        public UpImgSet UpImgSet { get; set; }
        /// <summary>
        /// 上传说明
        /// </summary>
        [LenValidate("file.user.dir.up.show.len", 0, 255)]
        public string UpShow { get; set; }
    }
}
