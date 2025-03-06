using Basic.HrCollect;
using Basic.HrModel.DB;
using Basic.HrModel.Dept;
using Basic.HrModel.DeptPrower;
using Basic.HrModel.Emp;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.DeptChange.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;

namespace Basic.HrService.lmpl
{
    internal class DeptChangeService : IDeptChangeService
    {
        private readonly IEmpCollect _Emp;
        private readonly IDeptCollect _Dept;
        private readonly ITitleDicItemCollect _Title;
        private readonly IEmpDeptProwerCollect _DeptPrower;
        private readonly IEmpTitleCollect _EmpTitle;
        private readonly IPostDicItemCollect _Post;
        public DeptChangeService (IEmpCollect emp,
            IDeptCollect dept,
            IEmpDeptProwerCollect deptPrower,
            ITitleDicItemCollect title,
            IPostDicItemCollect post,
            IEmpTitleCollect empTitle)
        {
            this._Post = post;
            this._DeptPrower = deptPrower;
            this._Emp = emp;
            this._Dept = dept;
            this._Title = title;
            this._EmpTitle = empTitle;
        }

        public void ToVoid (long deptId)
        {
            DBDept dept = this._Dept.Get(deptId);
            long[] ids = this._Dept.GetEnableSubId(dept).Add(deptId);
            if (this._Emp.CheckDeptIsExists(ids))
            {
                throw new ErrorException("hr.dept.emp.not.null");
            }
            this._Dept.ToVoidDept(ids);
        }
        public void MergeDept (long deptId, long toDeptId)
        {
            DBDept dept = this._Dept.Get(deptId);
            DBDept to = this._Dept.Get(toDeptId);
            if (dept.IsUnit != to.IsUnit)
            {
                throw new ErrorException("hr.dept.merge.type.error");
            }
            else if (dept.CompanyId != to.CompanyId)
            {
                throw new ErrorException("public.param.error");
            }
            long[] subDeptId = this._Dept.GetSubDeptId(dept.UnitId, dept).Add(dept.Id);
            var emps = this._Emp.GetEmps(new EmpGetParam
            {
                CompanyId = dept.CompanyId,
                DeptId = subDeptId,
                IsEntry = true
            }, a => new
            {
                a.EmpId,
                a.DeptId,
                a.IsOpenAccount
            });
            MergeDept merge = new MergeDept
            {
                ToDept = to,
                ToVoid = dept
            };
            if (!emps.IsNull())
            {
                merge.Emp = emps.ConvertAll(a =>
                {
                    long did = a.DeptId == deptId ? toDeptId : a.DeptId;
                    return new EmpDept
                    {
                        EmpId = a.EmpId,
                        DeptId = did,
                        IsOpenAccount = a.IsOpenAccount
                    };
                });
                long[] empId = emps.ConvertAll(a => a.EmpId);
                var title = this._EmpTitle.GetEmpDeptTitle(empId, subDeptId, a => new
                {
                    a.Id,
                    a.EmpId,
                    a.DeptId,
                    a.TitleCode
                });
                Dictionary<long, string[]> toTitle = this._EmpTitle.GetEmpDeptTitle(empId, toDeptId);

                merge.EmpTitle = title.Convert(a =>
                {
                    if (a.DeptId != toDeptId)
                    {
                        return true;
                    }
                    else if (toTitle.TryGetValue(a.EmpId, out string[] codes))
                    {
                        return !codes.Contains(a.TitleCode);
                    }
                    return true;
                }, a => new EmpTitleAdd
                {
                    EmpId = a.EmpId,
                    DeptId = a.DeptId == deptId ? toDeptId : a.DeptId,
                    TitleCode = a.TitleCode
                });
                merge.EmpTitleId = title.ConvertAll(a => a.Id);
                EmpDeptPrower[] prowers = this._DeptPrower.GetDeptPrower(empId, subDeptId.Add(toDeptId));
                if (!prowers.IsNull())
                {
                    merge.DropProwerId = prowers.ConvertAll(a => a.Id);
                    merge.Emp.ForEach(a =>
                    {
                        if (a.IsOpenAccount)
                        {
                            a.DeptPrower = prowers.Where(c => c.EmpId == a.EmpId && c.DeptId != deptId).Select(c => c.DeptId).Append(a.DeptId).Distinct().ToArray();
                        }
                    });
                }
            }
            this._Dept.Merge(merge);
        }
        public ChangeDeptTree GetDept (long deptId, bool? isUnit)
        {
            DBDept dept = this._Dept.Get(deptId);
            DeptBase[] depts = this._Dept.GetDepts(new DeptGetParam
            {
                IsAllChildren = true,
                Status = new HrDeptStatus[]
                {
                    HrDeptStatus.启用
                },
                CompanyId = dept.CompanyId,
                IsUnit = isUnit,
                ParentId = deptId
            });
            long[] ids = depts.ConvertAll(a => a.Id).Add(dept.Id);
            Dictionary<long, int> empNum = this._Emp.GetDeptEmpNum(ids);
            return new ChangeDeptTree
            {
                DeptName = dept.ShortName.GetValueOrDefault(dept.DeptName),
                EmpNum = empNum.GetValueOrDefault(deptId),
                Children = depts.ToTree(deptId, empNum),
                Id = dept.Id,
                IsUnit = dept.IsUnit,
            };
        }
        public DisbandedDeptEmp[] GetDisbandedDeptEmp (DeptDisbandedArg arg)
        {
            EmpDto[] emps = this._Emp.GetEmps<EmpDto>(new EmpGetParam
            {
                CompanyId = arg.CompanyId,
                DeptId = new long[]
                {
                        arg.DeptId
                },
                Status = new HrEmpStatus[]
                {
                        HrEmpStatus.启用,
                        HrEmpStatus.起草
                }
            });
            DisbandedDeptEmp[] dtos = emps.ConvertMap<EmpDto, DisbandedDeptEmp>();
            Dictionary<long, string[]> titles = this._EmpTitle.GetEmpDeptTitle(dtos.ConvertAll(a => a.EmpId), arg.DeptId);
            List<string> titleId = [];
            titles.ForEach((a, v) =>
            {
                titleId.AddRange(v);
            });
            string deptName = this._Dept.GetDeptName(arg.DeptId);
            long unitId = this._Dept.GetUnitId(arg.DeptId);
            string unitName = this._Dept.GetDeptName(unitId);
            Dictionary<string, string> titleName = this._Title.GetTitleNames(titleId.Distinct().ToArray());
            dtos.ForEach(c =>
            {
                c.Unit = unitName;
                c.UnitId = unitId;
                c.Dept = deptName;
                if (titles.TryGetValue(c.EmpId, out string[] title))
                {
                    c.TitleId = title;
                    c.Title = title.ConvertAll(a => titleName.GetValueOrDefault(a));
                }
            });
            return dtos;
        }
        public MergeEmp GetMergeEmp (DeptMergeArg arg)
        {
            EmpDto[] emps = this._Emp.GetEmps<EmpDto>(new EmpGetParam
            {
                CompanyId = arg.CompanyId,
                DeptId = new long[]
                {
                    arg.DeptId
                }
            });
            long unitId = this._Dept.GetUnitId(arg.DeptId);
            string name = this._Dept.GetDeptName(unitId);
            long toUnitId = this._Dept.GetUnitId(arg.ToDeptId);
            string toUnitName = this._Dept.GetDeptName(toUnitId);
            MergeEmp merge = new MergeEmp
            {
                UnitId = unitId,
                ToUnitId = toUnitId,
                DeptName = this._Dept.GetDeptName(arg.DeptId),
                ToDeptName = this._Dept.GetDeptName(arg.ToDeptId),
                ToUnitName = toUnitName,
                UnitName = name,
            };
            MergeEmpDto[] dtos = emps.ConvertMap<EmpDto, MergeEmpDto>();
            Dictionary<long, string[]> titles = this._EmpTitle.GetEmpTitle(dtos.ConvertAll(a => new KeyValuePair<long, long>(a.EmpId, a.DeptId)));
            List<string> titleId = [];
            titles.ForEach((a, v) =>
            {
                titleId.AddRange(v);
            });
            Dictionary<long, string[]> toTitle = this._EmpTitle.GetEmpDeptTitle(dtos.Convert(c => c.DeptId == arg.ToDeptId, c => c.EmpId), arg.ToDeptId);
            toTitle.ForEach((a, v) =>
            {
                titleId.AddRange(v);
            });
            Dictionary<string, string> titleName = this._Title.GetTitleNames(titleId.Distinct().ToArray());
            Dictionary<string, string> postName = this._Post.GetPostName(dtos.Convert(c => c.PostCode));
            dtos.ForEach(c =>
            {
                if (titles.TryGetValue(c.EmpId, out string[] title))
                {
                    c.TitleId = title;
                    c.Title = title.ConvertAll(a => titleName.GetValueOrDefault(a));
                }
                if (toTitle.TryGetValue(c.EmpId, out title))
                {
                    c.ToTitleId = title;
                    c.ToTitle = title.ConvertAll(a => titleName.GetValueOrDefault(a));
                }
                if (c.PostCode.IsNotNull())
                {
                    c.Post = postName.GetValueOrDefault(c.PostCode);
                }
            });
            merge.Emps = dtos;
            return merge;
        }
    }
}
