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
        public DeptTree[] GetTree ( UnitGetArg arg )
        {
            DeptBase[] depts = this._Dept.GetUnitDepts(new UnitGetParam
            {
                CompanyId = [arg.CompanyId],
                ParentId = arg.ParentId,
                IsAllChildren = true,
                UnitId = arg.UnitId,
                IsDept = arg.IsDept,
                IsUnit = arg.IsUnit,
                Status = arg.Status,
                DeptId = arg.DeptId,
            });
            return depts.ToTree();
        }
        private CompanyTree<DeptTree>[] _GetChildren ( CompanyName prt, CompanyName[] coms, DeptBase[] depts )
        {
            return coms.Convert(a => a.ParentId == prt.Id, a => new CompanyTree<DeptTree>
            {
                Id = a.Id,
                Name = a.ShortName.GetValueOrDefault(a.FullName),
                Dept = depts.FindAll(a => a.CompanyId == a.Id).ToTree(),
                Children = this._GetChildren(a, coms, depts)
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

        public CompanyTree<UnitTree> GetUnitTree ( UnitQueryParam arg )
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
            CompanyName com = coms.Find(a => a.Id == arg.CompanyId);
            CompanyTree<UnitTree> obj = new CompanyTree<UnitTree>
            {
                Id = com.Id,
                Name = com.ShortName.GetValueOrDefault(com.FullName),
                Dept = depts.FindAll(a => a.CompanyId == com.Id).ToUnitTree(arg.ParentId.GetValueOrDefault())
            };
            if ( coms.Length == 1 )
            {
                return obj;
            }
            obj.Children = this._GetUnitChildren(com, coms, depts);
            return obj;
        }
        private CompanyTree<UnitTree>[] _GetUnitChildren ( CompanyName prt, CompanyName[] coms, DeptBase[] depts )
        {
            return coms.Convert(a => a.ParentId == prt.Id, a => new CompanyTree<UnitTree>
            {
                Id = a.Id,
                Name = a.ShortName.GetValueOrDefault(a.FullName),
                Dept = depts.FindAll(a => a.CompanyId == a.Id).ToUnitTree(0),
                Children = this._GetUnitChildren(a, coms, depts)
            });
        }
    }
}
