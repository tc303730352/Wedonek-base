using Basic.HrModel.DB;
using WeDonekRpc.Client;

namespace Basic.HrLocalEvent.Model
{
    public class EmpLocalEvent : RpcLocalEvent
    {
        public EmpLocalEvent (DBEmpList emp)
        {
            this.Emp = emp;
        }
        public EmpLocalEvent (DBEmpList emp, string[] cols)
        {
            this.UpdateCol = cols;
            this.Emp = emp;
        }

        public DBEmpList Emp { get; }
        public string[] UpdateCol { get; }
    }
}
