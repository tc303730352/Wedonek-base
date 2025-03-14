using Basic.HrCollect;
using Basic.HrModel.DB;
using Basic.HrModel.Dept;
using Basic.HrRemoteModel.Dept.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;

namespace Basic.HrService.lmpl
{
    internal class DeptService : IDeptService
    {
        private readonly IDeptCollect _Dept;
        private readonly IEmpCollect _Emp;
        public DeptService ( IDeptCollect dept, IEmpCollect emp )
        {
            this._Emp = emp;
            this._Dept = dept;
        }

        public long Add ( DeptAdd add )
        {
            return this._Dept.Add(add);
        }

        public void Delete ( long id )
        {
            DBDept dept = this._Dept.Get(id);
            this._Dept.Delete(dept);
        }

        public bool Set ( long id, DeptSet set )
        {
            DBDept dept = this._Dept.Get(id);
            return this._Dept.Set(dept, set);
        }

        public bool Enable ( long[] deptId )
        {
            if ( deptId.Length == 1 )
            {
                DBDept dept = this._Dept.Get(deptId[0]);
                return this._Dept.Enable(dept);
            }
            return this._Dept.Enable(deptId);
        }
        public DeptSelect[] GetDeptSelect ( DeptGetArg arg )
        {
            DeptBase[] depts = this._Dept.GetDepts<DeptBase>(new DeptGetParam
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
        public DeptDto Get ( long id )
        {
            DBDept dept = this._Dept.Get(id);
            DeptDto dto = dept.ConvertMap<DBDept, DeptDto>();
            if ( dto.LeaderId.HasValue )
            {
                dto.LeaderName = this._Emp.GetName(dto.LeaderId.Value);
            }
            return dto;
        }

        public DeptTree[] GetTree ( DeptGetArg arg )
        {
            DeptBase[] depts = this._Dept.GetDepts<DeptBase>(new DeptGetParam
            {
                CompanyId = arg.CompanyId,
                ParentId = arg.ParentId,
                IsAllChildren = true,
                Status = arg.Status
            });
            return depts.ToTree();
        }
        public DeptTallyTree[] GetTallyTrees ( DeptGetArg arg )
        {
            DeptBaseDto[] depts = this._Dept.GetDepts<DeptBaseDto>(new DeptGetParam
            {
                CompanyId = arg.CompanyId,
                ParentId = arg.ParentId,
                IsAllChildren = true,
                Status = arg.Status
            });
            if ( depts.IsNull() )
            {
                return Array.Empty<DeptTallyTree>();
            }
            Dictionary<long, int> empNum = this._Emp.GetDeptEmpNum(depts.Convert(c => c.IsUnit == false, c => c.Id));
            Dictionary<long, string> empName = this._Emp.GetName(depts.Convert(c => c.IsUnit == false && c.LeaderId.HasValue, c => c.LeaderId.Value));
            return depts.ToTree(empNum, empName);
        }

        public bool Stop ( long deptId )
        {
            DBDept dept = this._Dept.Get(deptId);
            return this._Dept.Stop(dept);
        }

        public DeptFullTree[] GetDeptList ( DeptQueryParam query )
        {
            DBDept[] dept = this._Dept.Gets<DBDept>(query);
            if ( dept.Length != 0 )
            {
                dept = dept.OrderBy(a => a.Sort).ThenByDescending(a => a.IsUnit).ToArray();
            }
            if ( query.UnitId.HasValue )
            {
                DBDept unit = this._Dept.Get(query.UnitId.Value);
                Dictionary<long, string> emps = this._Emp.GetName(dept.Convert(c => c.LeaderId.HasValue, c => c.LeaderId.Value));
                DeptFullTree tree = unit.ConvertMap<DBDept, DeptFullTree>();
                if ( tree.LeaderId.HasValue )
                {
                    tree.LeaderName = this._Emp.GetName(tree.LeaderId.Value);
                }
                if ( dept.Length != 0 )
                {
                    tree.Children = this._GetChildren(tree, dept, emps);
                }
                return new DeptFullTree[] { tree };
            }
            else if ( dept.Length == 0 )
            {
                return Array.Empty<DeptFullTree>();
            }
            else
            {
                int level = dept.Min(c => c.Lvl);
                DeptFullTree[] dto = dept.ConvertMap<DBDept, DeptFullTree>(a => a.Lvl == level);
                Dictionary<long, string> emps = this._Emp.GetName(dept.Convert(c => c.LeaderId.HasValue, c => c.LeaderId.Value));
                dto.ForEach(c =>
                {
                    if ( c.LeaderId.HasValue )
                    {
                        c.LeaderName = emps.GetValueOrDefault(c.LeaderId.Value);
                    }
                    c.Children = this._GetChildren(c, dept, emps);
                });
                return dto;
            }
        }
        private DeptFullTree[] _GetChildren ( DeptFullTree tree, DBDept[] depts, Dictionary<long, string> emps )
        {
            DeptFullTree[] dto = depts.ConvertMap<DBDept, DeptFullTree>(a => a.ParentId == tree.Id);
            if ( dto.Length == 0 )
            {
                return Array.Empty<DeptFullTree>();
            }
            dto.ForEach(c =>
            {
                if ( c.LeaderId.HasValue )
                {
                    c.LeaderName = emps.GetValueOrDefault(c.LeaderId.Value);
                }
                c.Children = this._GetChildren(c, depts, emps);
            });
            return dto;
        }

        public void SetLeader ( long id, long? leaderId )
        {
            DBDept dept = this._Dept.Get(id);
            this._Dept.SetLeader(dept, leaderId);
        }
    }
}
