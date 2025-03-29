using Basic.HrGatewaryModular.Interface;
using WeDonekRpc.Client.Attr;
using WeDonekRpc.Client.Interface;
using WeDonekRpc.Helper.Config;

namespace Basic.HrGatewaryModular.Config
{
    public enum LogRecordRange
    {
        请求参数 = 2,
        响应结果 = 4,
        全部 = 6,
    }
    [ClassLifetimeAttr(ClassLifetimeType.SingleInstance)]
    internal class OperateLogConfig : IOperateLogConfig
    {
        public OperateLogConfig ( ISysConfig config )
        {
            IConfigSection section = config.GetSection("hr:operateLog");
            section.AddRefreshEvent(this._Init);
        }
        private void _Init ( IConfigSection config, string name )
        {
            this.IsEnable = config.GetValue<bool>("IsEnable", true);
            this.Range = config.GetValue<LogRecordRange>("RecordRange", LogRecordRange.全部);

        }
        public bool IsEnable { get; private set; }

        public LogRecordRange Range { get; private set; }
    }
}
