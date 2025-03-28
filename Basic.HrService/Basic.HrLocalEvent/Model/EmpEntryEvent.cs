using Basic.HrModel.DB;
using WeDonekRpc.Client;

namespace Basic.HrLocalEvent.Model
{
    public class EmpEntryEvent : RpcLocalEvent
    {
        public EmpEntryEvent ( DBEmpList emp, long comId )
        {
            this.CompanyId = comId;
            this.Emp = emp;
        }

        public DBEmpList Emp { get; }

        public long CompanyId { get; }
    }
}
