using Basic.HrRemoteModel.Dept.Model;
using Basic.HrRemoteModel.Unit;
using Basic.HrRemoteModel.Unit.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class UnitDeptEvent : IRpcApiService
    {
        private readonly IUnitDeptService _Service;

        public UnitDeptEvent ( IUnitDeptService service )
        {
            this._Service = service;
        }

        public DeptSelect[] GetUnitDeptSelect ( GetUnitDeptSelect arg )
        {
            return this._Service.GetDeptSelect(arg.Param);
        }

        public CompanyTree<DeptTree> GetUnitDeptTree ( GetUnitDeptTree obj )
        {
            return this._Service.GetTree(obj.Param);
        }

        public UnitSelect[] GetUnitSelect ( GetUnitSelect obj )
        {
            return this._Service.GetUnitSelect(obj.Param);
        }

        public CompanyTree<UnitTree> GetUnitTree ( GetUnitTree obj )
        {
            return this._Service.GetUnitTree(obj.Param);
        }
    }
}
