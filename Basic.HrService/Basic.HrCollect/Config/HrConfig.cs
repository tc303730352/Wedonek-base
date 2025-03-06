using WeDonekRpc.Client.Attr;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.Config;

namespace Basic.HrCollect.Config
{
    [ClassLifetimeAttr(ClassLifetimeType.SingleInstance)]
    internal class HrConfig : IHrConfig
    {
        private string _WebPwdSign;
        public HrConfig (IConfigCollect config)
        {
            IConfigSection section = config.GetSection("Dict");
            section.AddRefreshEvent(this._Init);
        }

        private void _Init (IConfigSection section, string name)
        {
            this.TitleDicId = section.GetValue<long>("TitleDicId", 1);
            this.PostDicId = section.GetValue<long>("PostDicId", 2);
            this.InitPwd = section.GetValue<string>("InitPwd", "000000");
            this._WebPwdSign = section.GetValue<string>("WebSign", "wedonekRpc_6xy");
            this.PwdSign = section.GetValue("PwdSign", "6xy^#5a%acde");
        }

        public string EncryptionPwd (string pwd, long empId)
        {
            return string.Join("_", pwd, this.PwdSign, empId).GetMd5();
        }
        public string EncryptionFullPwd (string pwd, long empId)
        {
            pwd = string.Concat(pwd, this._WebPwdSign);
            return this.EncryptionPwd(pwd, empId);
        }
        /// <summary>
        /// 人员职位字典ID
        /// </summary>
        public long TitleDicId { get; private set; }
        /// <summary>
        /// 岗位(职务)字典ID
        /// </summary>
        public long PostDicId { get; private set; }
        /// <summary>
        /// 密码签名
        /// </summary>
        public string PwdSign { get; private set; }
        /// <summary>
        /// 初始密码
        /// </summary>
        public string InitPwd { get; private set; }
    }
}
