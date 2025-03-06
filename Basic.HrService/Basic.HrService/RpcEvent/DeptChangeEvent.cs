using Basic.HrRemoteModel.DeptChange;
using Basic.HrRemoteModel.DeptChange.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class DeptChangeEvent : IRpcApiService
    {
        private readonly IDeptChangeService _Service;
        public DeptChangeEvent (IDeptChangeService service)
        {
            this._Service = service;
        }
        public void ToVoidDept (ToVoidDept obj)
        {
            this._Service.ToVoid(obj.DeptId);
        }
        public ChangeDeptTree GetChangeDept (GetChangeDept obj)
        {
            return this._Service.GetDept(obj.DeptId, obj.IsUnit);
        }
        public DisbandedDeptEmp[] GetDisbandedDeptEmp (GetDisbandedDeptEmp obj)
        {
            return this._Service.GetDisbandedDeptEmp(obj.Arg);
        }
        public MergeEmp GetMergeDeptEmp (GetMergeDeptEmp obj)
        {
            return this._Service.GetMergeEmp(obj.Arg);
        }

        public void MergeDept (MergeDept merge)
        {
            this._Service.MergeDept(merge.DeptId, merge.ToDeptId);
        }
    }
}
