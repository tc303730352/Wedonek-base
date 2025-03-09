using Basic.HrRemoteModel.OperatePower.Model;

namespace Basic.HrGatewaryModular.Model.OpPower
{
    public class OperatePower
    {
        /// <summary>
        /// 操作权限列表
        /// </summary>
        public OperatePowerBase[] Powers { get; set; }


        /// <summary>
        /// 选中的权限
        /// </summary>
        public long[] Selected { get; set; }
    }
}
