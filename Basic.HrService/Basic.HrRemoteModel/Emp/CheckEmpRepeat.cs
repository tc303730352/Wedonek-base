using Basic.HrRemoteModel.Emp.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Emp
{
    /// <summary>
    /// 检查人员数据是否重复
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class CheckEmpRepeat : RpcRemote<bool>
    {
        public DataRepeatCheck Check { get; set; }
    }
}
