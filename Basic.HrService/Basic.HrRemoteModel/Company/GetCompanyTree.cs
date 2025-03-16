using Basic.HrRemoteModel.Company.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Company
{
    /// <summary>
    /// 获取公司列表树形结构
    /// </summary>
    [IRemoteConfig("basic.hr.service")]
    public class GetCompanyTree : RpcRemoteArray<CompanyTree>
    {
    }
}
