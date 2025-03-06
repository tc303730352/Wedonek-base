using Basic.HrRemoteModel.Dept;
using Basic.HrRemoteModel.Dept.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class DeptEvent : IRpcApiService
    {
        private readonly IDeptService _Service;

        public DeptEvent (IDeptService service)
        {
            this._Service = service;
        }
        public DeptFullTree[] GetDeptList (GetDeptList obj)
        {
            return this._Service.GetDeptList(obj.Query);
        }
        public void SetDeptLeader (SetDeptLeader obj)
        {
            this._Service.SetLeader(obj.Id, obj.LeaderId);
        }
        public long AddDept (AddDept add)
        {
            return this._Service.Add(add.Datum);
        }

        public void DeleteDept (DeleteDept obj)
        {
            this._Service.Delete(obj.Id);
        }

        public bool EnableDept (EnableDept obj)
        {
            return this._Service.Enable(obj.DeptId);
        }
        public DeptSelect[] GetDeptSelect (GetDeptSelect arg)
        {
            return this._Service.GetDeptSelect(arg.GetParam);
        }
        public DeptDto GetDept (GetDept obj)
        {
            return this._Service.Get(obj.Id);
        }

        public DeptTree[] GetDeptTree (GetDeptTree obj)
        {
            return this._Service.GetTree(obj.Param);
        }

        public bool SetDept (SetDept set)
        {
            return this._Service.Set(set.Id, set.Dept);
        }

        public bool StopDept (StopDept obj)
        {
            return this._Service.Stop(obj.DeptId);
        }
    }
}
