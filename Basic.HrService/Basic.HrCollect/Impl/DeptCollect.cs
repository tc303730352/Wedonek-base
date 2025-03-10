﻿using System.Text;
using Basic.HrCollect.Model;
using Basic.HrDAL;
using Basic.HrModel.DB;
using Basic.HrModel.Dept;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Dept.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;

namespace Basic.HrCollect.Impl
{
    internal class DeptCollect : IDeptCollect
    {
        private readonly IDeptDAL _Dept;

        public DeptCollect (IDeptDAL dept)
        {
            this._Dept = dept;
        }
        public long[] GetEnableDeptId (long unitId)
        {
            return this._Dept.GetEnableDeptId(unitId);
        }
        public Result[] Gets<Result> (DeptQueryParam query) where Result : class, new()
        {
            return this._Dept.Gets<Result>(query);
        }
        public string GetDeptName (long id)
        {
            return this._Dept.GetDeptName(id);
        }
        public string GetUnitDeptName (long id)
        {
            return this._Dept.GetUnitDeptName(id);
        }
        public Dictionary<long, string> GetDeptName (long[] ids)
        {
            return this._Dept.GetDeptName(ids);
        }
        public DBDept Get (long id)
        {
            return this._Dept.Get(id);
        }
        public DeptBase[] GetDepts (DeptGetParam param)
        {
            return this._Dept.GetDepts<DeptBase>(param);
        }
        public long[] GetEnableSubId (DBDept dept)
        {
            return this._Dept.GetsSubId(dept, HrDeptStatus.启用);
        }
        public long[] GetSubDeptId (long unitId, DBDept dept)
        {
            return this._Dept.GetSubDeptId(unitId, dept);
        }
        public DeptBase[] GetUnitDepts (UnitGetParam param)
        {
            return this._Dept.GetUnitDepts<DeptBase>(param);
        }
        public long Add (DeptAdd add)
        {
            if (this._Dept.CheckRepeat(add.CompanyId, add.ParentId, add.DeptName))
            {
                throw new ErrorException("hr.dept.name.repeat");
            }
            else if (!add.ShortName.IsNull() && this._Dept.CheckShortRepeat(add.CompanyId, add.ParentId, add.ShortName))
            {
                throw new ErrorException("hr.dept.short.name.repeat");
            }
            DBDept dept = add.ConvertMap<DeptAdd, DBDept>();
            dept.Status = HrDeptStatus.起草;
            if (add.ParentId != 0)
            {
                var prt = this._Dept.Get(add.ParentId, a => new
                {
                    a.Lvl,
                    a.LevelCode,
                    a.UnitId,
                    a.IsUnit
                });
                if (prt.Lvl == 1)
                {
                    dept.LevelCode = "|" + add.ParentId + "|";
                }
                else
                {
                    dept.LevelCode = prt.LevelCode + add.ParentId + "|";
                }
                dept.Lvl = prt.Lvl + 1;
                if (!dept.IsUnit)
                {
                    dept.UnitId = prt.IsUnit ? add.ParentId : prt.UnitId;
                }
            }
            else
            {
                dept.LevelCode = string.Empty;
                dept.Lvl = 1;
                dept.IsUnit = true;
            }
            dept.Sort = this._Dept.GetMaxSort(add.CompanyId, add.ParentId) + 1;
            this._Dept.AddDept(dept);
            return dept.Id;
        }
        public void Delete (DBDept dept)
        {
            if (dept.Status != HrDeptStatus.起草)
            {
                throw new ErrorException("hr.dept.not.allow.delete");
            }
            long[] subId = this._Dept.GetsSubId(dept);
            this._Dept.Delete(subId.Add(dept.Id));
        }
        public bool Enable (DBDept dept)
        {
            if (dept.Status == HrDeptStatus.启用)
            {
                return false;
            }
            else if (dept.ParentId != 0)
            {
                HrDeptStatus status = this._Dept.Get(dept.ParentId, a => a.Status);
                if (status != HrDeptStatus.启用)
                {
                    throw new ErrorException("hr.parent.dept.no.enable");
                }
            }
            this._Dept.EnableDept(dept);
            return true;
        }
        public bool Enable (long[] deptId)
        {
            var pdept = this._Dept.Gets(deptId, a => new
            {
                a.ParentId,
                a.Status
            });
            if (!pdept.IsNull() && pdept.IsExists(a => !deptId.Contains(a.ParentId) && a.Status != HrDeptStatus.启用))
            {
                throw new ErrorException("hr.parent.dept.no.enable");
            }
            long[] ids = pdept.ConvertAll(a => a.ParentId);
            deptId = deptId.Remove(ids);
            this._Dept.EnableDept(deptId);
            return true;
        }
        public bool Stop (DBDept dept)
        {
            if (dept.Status == HrDeptStatus.停用)
            {
                return false;
            }
            long[] subId = this._Dept.GetsSubId(dept, HrDeptStatus.启用);
            this._Dept.StopDept(subId.Add(dept.Id));
            return true;
        }

        public bool Set (DBDept dept, DeptSet set)
        {
            if (set.DeptName != dept.DeptName && this._Dept.CheckRepeat(dept.CompanyId, dept.ParentId, set.DeptName))
            {
                throw new ErrorException("hr.dept.name.repeat");
            }
            else if (set.ShortName.IsNotNull() && set.ShortName != dept.ShortName && this._Dept.CheckShortRepeat(dept.CompanyId, dept.ParentId, set.ShortName))
            {
                throw new ErrorException("hr.dept.short.name.repeat");
            }
            else if (dept.IsUnit == false && set.ParentId == 0)
            {
                throw new ErrorException("hr.dept.parent.id.null");
            }
            if (set.ParentId != dept.ParentId)
            {
                var prt = this._Dept.Get(set.ParentId, a => new
                {
                    a.UnitId,
                    a.IsUnit,
                    a.LevelCode,
                    a.Lvl
                });
                if (( prt.IsUnit && set.ParentId != dept.UnitId ) || ( prt.IsUnit == false && prt.UnitId != dept.UnitId ))
                {
                    throw new ErrorException("hr.dept.unit.id.not.agreement");
                }
                DeptSetArg arg = new DeptSetArg
                {
                    DeptName = set.DeptName,
                    ShortName = set.ShortName,
                    DeptShow = set.DeptShow,
                    DeptTag = set.DeptTag.Join('|', '|'),
                    LevelCode = prt.LevelCode + set.ParentId + "|",
                    Sort = this._Dept.GetMaxSort(dept.CompanyId, set.ParentId) + 1,
                    Lvl = prt.Lvl + 1,
                    LeaderId = set.LeaderId,
                    ParentId = set.ParentId
                };
                string level = dept.LevelCode + dept.Id + "|";
                var sub = this._Dept.Gets(a => a.LevelCode.StartsWith(level), a => new
                {
                    a.Id,
                    a.LevelCode,
                    a.Lvl
                });
                SubDeptSet[] sets = null;
                if (!sub.IsNull())
                {
                    int lvl = prt.Lvl - dept.Lvl;
                    sets = sub.ConvertAll(c =>
                    {
                        return new SubDeptSet
                        {
                            Id = c.Id,
                            LevelCode = prt.LevelCode + c.LevelCode.Substring(dept.LevelCode.Length),
                            Lvl = c.Lvl + lvl,
                        };
                    });
                }
                this._Dept.Set(dept, arg, sets);
                return true;
            }
            else
            {
                return this._Dept.Set(dept, set);
            }
        }

        public Dictionary<long, string> GetDeptFullName (long[] ids)
        {
            if (ids.IsNull())
            {
                return null;
            }
            DeptTemp[] depts = this._Dept.Gets(ids, a => new DeptTemp
            {
                Id = a.Id,
                DeptName = a.DeptName,
                ShortName = a.ShortName,
                LevelCode = a.LevelCode
            });
            if (depts.IsNull())
            {
                return null;
            }
            List<long> pid = [];
            depts.ForEach(a =>
            {
                if (a.LevelCode != string.Empty)
                {
                    a.Pid = a.LevelCode.Remove(a.LevelCode.Length - 1, 1).Remove(0, 3).Split('|').ConvertAll(a => long.Parse(a));
                    pid.AddRange(a.Pid);
                }
            });
            if (pid.Count == 0)
            {
                return depts.ToDictionary(a => a.Id, a => a.ShortName.GetValueOrDefault(a.DeptName));
            }
            Dictionary<long, string> pName = this._Dept.GetDeptName(pid.Distinct().ToArray());
            return depts.ToDictionary(a => a.Id, a =>
            {
                if (a.Pid == null)
                {
                    return a.ShortName.GetValueOrDefault(a.DeptName);
                }
                StringBuilder str = new StringBuilder();
                a.Pid.ForEach(c =>
                {
                    _ = str.Append(pName.GetValueOrDefault(c, string.Empty));
                    _ = str.Append(">");
                });
                _ = str.Append(a.ShortName.GetValueOrDefault(a.DeptName));
                return str.ToString();
            });
        }
        public long GetUnitId (long deptId)
        {
            return this._Dept.Get(deptId, a => a.UnitId);
        }
        public void SetLeader (DBDept dept, long? leaderId)
        {
            if (dept.LeaderId == leaderId)
            {
                return;
            }
            this._Dept.SetLeader(dept, leaderId);
        }
        public void Merge (MergeDept merge)
        {
            string level = merge.ToVoid.LevelCode + merge.ToVoid.Id + "|";
            var sub = this._Dept.Gets(a => a.LevelCode.StartsWith(level), a => new
            {
                a.Id,
                a.ParentId,
                a.LevelCode,
                a.Lvl
            });
            MergeSubDeptSet[] subs = null;
            if (!sub.IsNull())
            {
                string newLevel = merge.ToDept.LevelCode + merge.ToDept.Id + "|";
                subs = sub.ConvertAll(c =>
                {
                    return new MergeSubDeptSet
                    {
                        Id = c.Id,
                        LevelCode = newLevel + c.LevelCode.Substring(level.Length),
                        Lvl = merge.ToDept.Lvl + ( c.Lvl - merge.ToVoid.Lvl ),
                        ParentId = c.ParentId == merge.ToVoid.Id ? merge.ToVoid.Id : c.ParentId,
                    };
                });
            }
            this._Dept.Merge(merge, subs);
        }

        public void ToVoidDept (long[] deptId)
        {
            this._Dept.ToVoidDept(deptId);
        }

        public KeyValuePair<long, long>[] GetUnitId (long[] deptId)
        {
            return this._Dept.Gets(a => deptId.Contains(a.Id), a => new
            {
                a.Id,
                a.UnitId
            }).ConvertAll(a => new KeyValuePair<long, long>(a.Id, a.UnitId));
        }
    }
}
