using Basic.HrRemoteModel.Emp.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Model;

namespace Basic.HrRemoteModel.Emp
{
    [IRemoteConfig("basic.hr.service")]
    public class GetEmpBasics : RpcRemoteArray<EmpBasicDatum>
    {
        public long CompanyId
        {
            get;
            set;
        }
        public long[] EmpId
        {
            get;
            set;
        }
    }
}
