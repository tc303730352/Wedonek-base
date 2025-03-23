using Basic.HrCollect;
using Basic.HrModel.Company;
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
        private readonly ICompanyCollect _Company;
        public UnitDeptService ( IDeptCollect dept, ICompanyCollect company )
        {
            this._Company = company;
            this._Dept = dept;
        }
        private CompanyName[] _GetCompanys ( long comId, bool IsSub )
        {
            CompanyName name = this._Company.Get<CompanyName>(comId);
            if ( IsSub )
            {
                string code = this._Company.Get(comId, a => a.LevelCode);
                code = ( code == string.Empty ? "|" : code ) + comId + "|";
                return this._Company.GetSubs(code).Add(name);
            }
            else
            {
                return new CompanyName[] { name };
            }
        }
        public CompanyTree<DeptTree>[] GetTree ( UnitGetArg arg )
        {
            CompanyName[] coms = this._GetCompanys(arg.CompanyId, arg.IsSubCompany);
            DeptBase[] depts = this._Dept.GetUnitDepts(new UnitGetParam
            {
                CompanyId = coms.ConvertAll(a => a.Id),
                ParentId = arg.ParentId,
                IsAllChildren = true,
                UnitId = arg.UnitId,
                IsDept = arg.IsDept,
                IsUnit = arg.IsUnit,
                Status = arg.Status,
                DeptId = arg.DeptId,
            });
            return coms.Convert<CompanyName, CompanyTree<DeptTree>>(a =>
            {
                DeptTree[] list = depts.FindAll(a => a.CompanyId == a.Id).ToTree();
                if ( list.Length == 0 )
                {
                    return null;
                }
                return new CompanyTree<DeptTree>
                {
                    Id = a.Id,
                    Name = a.ShortName.GetValueOrDefault(a.FullName),
                    Children = list,
                };
            });
        }
        public DeptSelect[] GetDeptSelect ( DeptSelectGetArg arg )
        {
            DeptBase[] depts = this._Dept.GetUnitDepts(new UnitGetParam
            {
                CompanyId = new long[] { arg.CompanyId },
                ParentId = arg.ParentId,
                IsAllChildren = false,
                Status = arg.Status,
                DeptId = arg.DeptId,
            });
            return depts.ConvertAll(a => new DeptSelect
            {
                DeptId = a.Id,
                DeptName = a.ShortName.GetValueOrDefault(a.DeptName),
                LeaderId = a.LeaderId
            });
        }

        public UnitSelect[] GetUnitSelect ( UnitSelectGetParam arg )
        {
            DeptBase[] depts = this._Dept.GetDepts<DeptBase>(new DeptGetParam
            {
                CompanyId = new long[] { arg.CompanyId },
                ParentId = arg.ParentId,
                Status = arg.Status,
                IsAllChildren = false,
                IsUnit = true,
                DeptId = arg.DeptId,
            });
            return depts.ConvertAll(a => new UnitSelect
            {
                DeptId = a.Id,
                DeptName = a.ShortName.GetValueOrDefault(a.DeptName),
                LeaderId = a.LeaderId
            });
        }

        public CompanyTree<UnitTree>[] GetUnitTree ( UnitQueryParam arg )
        {
            CompanyName[] coms = this._GetCompanys(arg.CompanyId, arg.IsSubCompany);
            DeptBase[] depts = this._Dept.GetDepts<DeptBase>(new DeptGetParam
            {
                CompanyId = coms.ConvertAll(a => a.Id),
                ParentId = arg.ParentId,
                IsAllChildren = true,
                Status = arg.Status,
                IsUnit = true,
                DeptId = arg.DeptId,
            });
            return coms.Convert<CompanyName, CompanyTree<UnitTree>>(a =>
            {
                UnitTree[] list = depts.FindAll(a => a.CompanyId == a.Id).ToUnitTree(arg.ParentId.GetValueOrDefault());
                if ( list.Length == 0 )
                {
                    return null;
                }
                return new CompanyTree<UnitTree>
                {
                    Id = a.Id,
                    Name = a.ShortName.GetValueOrDefault(a.FullName),
                    Children = list,
                };
            });
        }
    }
}
