using Basic.HrRemoteModel.Company.Model;
using WeDonekRpc.Client;

namespace Basic.HrRemoteModel.Company
{
    public class GetCompanyTreeItems : RpcRemoteArray<CompanyTreeItem>
    {
        public long? ParentId
        {
            get;
            set;
        }
    }
}
