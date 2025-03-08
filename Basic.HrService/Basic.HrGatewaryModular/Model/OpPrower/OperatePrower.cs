using Basic.HrRemoteModel.OperatePrower.Model;

namespace Basic.HrGatewaryModular.Model.OpPrower
{
    public class OperatePrower
    {
        /// <summary>
        /// 操作权限列表
        /// </summary>
        public OperateProwerBase[] Prowers { get; set; }


        /// <summary>
        /// 选中的权限
        /// </summary>
        public long[] Selected { get; set; }
    }
}
