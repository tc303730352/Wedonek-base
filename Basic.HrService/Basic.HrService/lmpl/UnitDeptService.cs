using Basic.HrCollect;
using Basic.HrModel.Dept;
using Basic.HrRemoteModel.Dept.Model;
using Basic.HrRemoteModel.Unit.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Helper;

namespace Basic.HrService.lmpl
{
    internal class UnitDeptService : IUnitDeptService
    {
        private readonly IDeptCollect _Dept;
        public UnitDeptService ( IDeptCollect dept )
        {
            this._Dept = dept;
        }
        public DeptTree[] GetTree ( UnitGetArg arg )
        {
            DeptBase[] depts = this._Dept.GetUnitDepts(new UnitGetParam
            {
                CompanyId = arg.CompanyId,
                ParentId = arg.ParentId,
                IsAllChildren = true,
                UnitId = arg.UnitId,
                IsDept = arg.IsDept,
                IsUnit = arg.IsUnit,
                Status = arg.Status
            });
            return depts.ToTree();
        }
        public DeptSelect[] GetDeptSelect ( UnitGetArg arg )
        {
            DeptBase[] depts = this._Dept.GetUnitDepts(new UnitGetParam
            {
                CompanyId = arg.CompanyId,
                ParentId = arg.ParentId,
                IsAllChildren = false,
                Status = arg.Status
            });
            return depts.ConvertAll(a => new DeptSelect
            {
                DeptId = a.Id,
                DeptName = a.ShortName.GetValueOrDefault(a.DeptName),
                LeaderId = a.LeaderId
            });
        }

        public UnitSelect[] GetUnitSelect ( UnitQueryParam arg )
        {
            DeptBase[] depts = this._Dept.GetDepts<DeptBase>(new DeptGetParam
            {
                CompanyId = arg.CompanyId,
                ParentId = arg.ParentId,
                Status = arg.Status,
                IsAllChildren = false,
                IsUnit = true
            });
            return depts.ConvertAll(a => new UnitSelect
            {
                DeptId = a.Id,
                DeptName = a.ShortName.GetValueOrDefault(a.DeptName),
                LeaderId = a.LeaderId
            });
        }

        public UnitTree[] GetUnitTree ( UnitQueryParam arg )
        {
            DeptBase[] depts = this._Dept.GetDepts<DeptBase>(new DeptGetParam
            {
                CompanyId = arg.CompanyId,
                ParentId = arg.ParentId,
                IsAllChildren = true,
                Status = arg.Status,
                IsUnit = true
            });
            return depts.ToUnitTree(arg.ParentId.GetValueOrDefault());
        }
    }
}
