using Basic.HrModel.DB;
using WeDonekRpc.Client;

namespace Basic.HrLocalEvent.Model
{
    public class CompanyEvent : RpcLocalEvent
    {
        public CompanyEvent ( DBCompany comp )
        {
            this.Company = comp;
        }
        public DBCompany Company { get; set; }
    }
}
