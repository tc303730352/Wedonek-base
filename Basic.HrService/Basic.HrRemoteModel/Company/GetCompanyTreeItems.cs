using Basic.HrRemoteModel.Company.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Company
{
    [IRemoteConfig("basic.hr.service")]
    public class GetCompanyTreeItems : RpcRemoteArray<CompanyTreeItem>
    {
        public long? ParentId
        {
            get;
            set;
        }
    }
}
