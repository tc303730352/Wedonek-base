using Basic.HrGatewaryModular.Config;

namespace Basic.HrGatewaryModular.Interface
{
    public interface IOperateLogConfig
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        bool IsEnable { get; }

        /// <summary>
        /// 记录范围
        /// </summary>
        LogRecordRange Range { get; }
    }
}