﻿using Basic.HrDAL.Model;
using Basic.HrModel.DB;
using Basic.HrModel.Dept;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Dept.Model;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.Model;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class DeptDAL : BasicDAL<DBDept, long>, IDeptDAL
    {
        public DeptDAL ( IRepository<DBDept> basicDAL ) : base(basicDAL)
        {
        }
        public Result[] Gets<Result> ( DeptQueryParam query ) where Result : class, new()
        {
            return this._BasicDAL.Gets<Result>(query.ToWhere(this._BasicDAL));
        }
        public Result[] Query<Result> ( DeptQueryParam query, IBasicPage paging, out int count ) where Result : class
        {
            paging.InitOrderBy("Id", true);
            return this._BasicDAL.Query<Result>(query.ToWhere(this._BasicDAL), paging, out count);
        }
        public void AddDept ( DBDept dept )
        {
            dept.Id = IdentityHelper.CreateId();
            if ( dept.UnitId == 0 && dept.IsUnit )
            {
                dept.UnitId = dept.Id;
            }
            this._BasicDAL.Insert(dept);
        }
        public bool CheckShortRepeat ( long companyId, long parentId, string shortName )
        {
            return this._BasicDAL.IsExist(c => c.CompanyId == companyId && c.ParentId == parentId && c.ShortName == shortName && c.IsToVoid == false);
        }
        public bool CheckRepeat ( long companyId, long parentId, string deptName )
        {
            return this._BasicDAL.IsExist(c => c.CompanyId == companyId && c.ParentId == parentId && c.DeptName == deptName && c.IsToVoid == false);
        }
        public string GetUnitDeptName ( long id )
        {
            var dept = this._BasicDAL.Get(a => a.Id == id, a => new
            {
                a.IsUnit,
                a.UnitId,
                a.DeptName,
                a.ShortName
            });
            if ( dept.IsUnit )
            {
                return dept.ShortName.GetValueOrDefault(dept.DeptName);
            }
            string unit = this.GetDeptName(dept.UnitId);
            return unit + "-" + dept.ShortName.GetValueOrDefault(dept.DeptName);
        }
        public string GetDeptName ( long id )
        {
            var dept = this._BasicDAL.Get(a => a.Id == id, a => new
            {
                a.DeptName,
                a.ShortName
            });
            return dept.ShortName.GetValueOrDefault(dept.DeptName);
        }
        public Dictionary<long, string> GetDeptName ( long[] ids )
        {
            return this._BasicDAL.Gets(a => ids.Contains(a.Id), a => new
            {
                a.Id,
                a.ShortName,
                a.DeptName
            }).ToDictionary(a => a.Id, a => a.ShortName.GetValueOrDefault(a.DeptName));
        }


        public void Delete ( long[] ids )
        {
            if ( !this._BasicDAL.Delete(a => ids.Contains(a.Id)) )
            {
                throw new ErrorException("hr.dept.delete.fail");
            }
        }

        public int GetMaxSort ( long companyId, long parentId )
        {
            return this._BasicDAL.Max(a => a.CompanyId == companyId && a.ParentId == parentId && a.IsToVoid == false, a => a.Sort);
        }

        public long[] GetsSubId ( DBDept dept )
        {
            string level = dept.LevelCode + dept.Id + "|";
            return this._BasicDAL.Gets(a => a.LevelCode.StartsWith(level) && a.IsToVoid == false, a => a.Id);
        }

        public long[] GetsSubId ( DBDept dept, HrDeptStatus status )
        {
            string level = dept.LevelCode + dept.Id + "|";
            return this._BasicDAL.Gets(a => a.LevelCode.StartsWith(level) && a.Status == status && a.IsToVoid == false, a => a.Id);
        }
        public long[] GetSubDeptId ( long unitId, DBDept dept )
        {
            string level = dept.LevelCode + dept.Id + "|";
            return this._BasicDAL.Gets(a => a.UnitId == unitId && a.LevelCode.StartsWith(level) && a.IsUnit == false && a.IsToVoid == false, a => a.Id);
        }


        public void EnableDept ( long[] ids )
        {
            if ( !this._BasicDAL.Update(a => a.Status == HrDeptStatus.启用, a => ids.Contains(a.Id)) )
            {
                throw new ErrorException("hr.dept.enable.fail");
            }
        }

        public void EnableDept ( DBDept dept )
        {
            if ( !this._BasicDAL.Update(a => a.Status == HrDeptStatus.启用, a => a.Id == dept.Id) )
            {
                throw new ErrorException("hr.dept.enable.fail");
            }
        }

        public void StopDept ( long[] ids )
        {
            if ( !this._BasicDAL.Update(a => a.Status == HrDeptStatus.停用, a => ids.Contains(a.Id)) )
            {
                throw new ErrorException("hr.dept.stop.fail");
            }
        }

        public T[] GetDepts<T> ( DeptGetParam param ) where T : class, new()
        {
            return this._BasicDAL.Gets<T>(param.ToWhere(this._BasicDAL));
        }
        public T[] GetUnitDepts<T> ( UnitGetParam param ) where T : class, new()
        {
            return this._BasicDAL.Gets<T>(param.ToWhere(this._BasicDAL));
        }
        public long[] GetEnableDeptId ( long unitId )
        {
            return this._BasicDAL.Gets(a => a.UnitId == unitId && a.Status == HrDeptStatus.启用 && a.IsToVoid == false, a => a.Id);
        }
        public bool Set ( DBDept dept, DeptSet set )
        {
            return this._BasicDAL.Update(dept, new DeptSetDto
            {
                DeptName = set.DeptName,
                DeptShow = set.DeptShow,
                ShortName = set.ShortName,
                LeaderId = set.LeaderId,
                DeptTag = set.DeptTag.Join('|', '|')
            });
        }
        public void Set ( DBDept dept, DeptSetArg arg, SubDeptSet[] sets )
        {
            if ( sets.IsNull() )
            {
                if ( !this._BasicDAL.Update(dept, arg) )
                {
                    throw new ErrorException("hr.dept.set.fail");
                }
                return;
            }
            ISqlQueue<DBDept> queue = this._BasicDAL.BeginQueue();
            _ = queue.Update(dept, arg);
            queue.UpdateBy<DBDept>(sets.ConvertAll(a => new DBDept
            {
                Id = a.Id,
                LevelCode = a.LevelCode,
                Lvl = a.Lvl,
            }), "LevelCode", "Lvl");
            _ = queue.Submit();
        }
        public void SetLeader ( DBDept dept, long? leaderId )
        {
            if ( !this._BasicDAL.Update(a => a.LeaderId == leaderId, a => a.Id == dept.Id) )
            {
                throw new ErrorException("hr.dept.leader.set.fail");
            }
        }
        public void Merge ( MergeDept merge, MergeSubDeptSet[] subs )
        {
            ISqlQueue<DBDept> queue = this._BasicDAL.BeginQueue();
            queue.UpdateOneColumn(a => a.IsToVoid == true, a => a.Id == merge.ToVoid.Id);
            if ( !subs.IsNull() )
            {
                queue.UpdateBy(subs, "LevelCode", "Lvl", "ParentId");
            }
            if ( !merge.DropPowerId.IsNull() )
            {
                queue.DeleteBy<DBEmpDeptPower>(a => merge.DropPowerId.Contains(a.Id));
            }
            if ( !merge.Emp.IsNull() )
            {
                queue.UpdateBy<DBEmpList>(merge.Emp.ConvertAll(a => new DBEmpList
                {
                    EmpId = a.EmpId,
                    DeptId = a.DeptId,
                    UnitId = merge.ToDept.UnitId,
                }), "DeptId", "UnitId");
                if ( !merge.EmpTitleId.IsNull() )
                {
                    queue.DeleteBy<DBEmpTitle>(a => merge.EmpTitleId.Contains(a.Id));
                }
                if ( !merge.EmpTitle.IsNull() )
                {
                    queue.InsertBy<DBEmpTitle>(merge.EmpTitle.ConvertAll(a => new DBEmpTitle
                    {
                        CompanyId = merge.ToDept.CompanyId,
                        Id = IdentityHelper.CreateId(),
                        EmpId = a.EmpId,
                        UnitId = merge.ToDept.UnitId,
                        DeptId = a.DeptId,
                        TitleCode = a.TitleCode
                    }));
                }

                List<DBEmpDeptPower> adds = [];
                merge.Emp.ForEach(c =>
                {
                    if ( !c.IsOpenAccount )
                    {
                        return;
                    }
                    else if ( c.DeptPower.IsNull() )
                    {
                        adds.Add(new DBEmpDeptPower
                        {
                            CompanyId = merge.ToDept.CompanyId,
                            Id = IdentityHelper.CreateId(),
                            UnitId = merge.ToDept.UnitId,
                            DeptId = c.DeptId,
                            EmpId = c.EmpId
                        });
                    }
                    else
                    {
                        c.DeptPower.ForEach(a =>
                        {
                            adds.Add(new DBEmpDeptPower
                            {
                                CompanyId = merge.ToDept.CompanyId,
                                Id = IdentityHelper.CreateId(),
                                UnitId = merge.ToDept.UnitId,
                                DeptId = a,
                                EmpId = c.EmpId
                            });
                        });
                    }
                });
                if ( adds.Count > 0 )
                {
                    queue.InsertBy<DBEmpDeptPower>(adds);
                }
            }
            _ = queue.Submit();
        }

        public void ToVoidDept ( long[] deptId )
        {
            ISqlQueue<DBDept> queue = this._BasicDAL.BeginQueue();
            queue.UpdateOneColumn(a => a.IsToVoid == true, a => deptId.Contains(a.Id));
            queue.DeleteBy<DBEmpDeptPower>(a => deptId.Contains(a.Id));
            _ = queue.Submit();
        }
    }
}
