using Basic.HrRemoteModel.Prower.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Prower
{
    /// <summary>
    /// 获取目录权限树含子系统
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetProwerTreeBySystem : RpcRemoteArray<ProwerSubSystem>
    {
    }
}
