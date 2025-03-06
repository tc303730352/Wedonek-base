using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.Dic.Model
{
    public class DicSet
    {
        /// <summary>
        /// 字典名
        /// </summary>
        [NullValidate("hr.dic.name.null")]
        [LenValidate("hr.dic.name.len", 2, 50)]
        public string DicName
        {
            get;
            set;
        }

        /// <summary>
        /// 备注说明
        /// </summary>
        [LenValidate("hr.dic.remark.len", 0, 255)]
        public string Remark { get; set; }
    }
}
