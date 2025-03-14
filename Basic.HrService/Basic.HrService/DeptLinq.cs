using Basic.HrModel.Dept;
using Basic.HrRemoteModel.Dept.Model;
using Basic.HrRemoteModel.DeptChange.Model;
using Basic.HrRemoteModel.Unit.Model;
using WeDonekRpc.Helper;

namespace Basic.HrService
{
    internal static class DeptLinq
    {
        public static ChangeDeptTree[] ToTree ( this DeptBase[] depts, long parentId, Dictionary<long, int> empNum )
        {
            return depts.Convert(a => a.ParentId == parentId, a => new ChangeDeptTree
            {
                Id = a.Id,
                DeptName = a.ShortName.GetValueOrDefault(a.DeptName),
                IsUnit = a.IsUnit,
                Children = ToTree(depts, a.Id, empNum)
            });
        }
        public static DeptTree[] ToTree ( this DeptBase[] depts )
        {
            int lvl = depts.Min(a => a.Lvl);
            return depts.Convert(a => a.Lvl == lvl, a => new DeptTree
            {
                DeptId = a.Id,
                DeptName = a.ShortName.GetValueOrDefault(a.DeptName),
                LeaderId = a.LeaderId,
                IsUnit = a.IsUnit,
                UnitId = a.UnitId,
                Children = _GetChildren(a, depts)
            });
        }
        public static DeptTallyTree[] ToTree ( this DeptBaseDto[] depts, Dictionary<long, int> empNum, Dictionary<long, string> empName )
        {
            int lvl = depts.Min(a => a.Lvl);
            return depts.Convert(a => a.Lvl == lvl, a =>
            {
                DeptTallyTree tree = new DeptTallyTree
                {
                    Id = a.Id,
                    Name = a.ShortName.GetValueOrDefault(a.DeptName),
                    IsUnit = a.IsUnit,
                    LeaderId = a.LeaderId,
                    DeptTag = a.DeptTag.ToSplit(),
                    DeptShow = a.DeptShow,
                    Children = _GetChildren(a, depts, empNum, empName)
                };
                if ( a.LeaderId.HasValue )
                {
                    tree.LeaderName = empName.GetValueOrDefault(a.LeaderId.Value);
                }
                if ( !a.IsUnit )
                {
                    tree.EmpNum = empNum.GetValueOrDefault(a.Id);
                }
                else if ( tree.Children.Length > 0 )
                {
                    tree.EmpNum = tree.Children.Sum(c => c.EmpNum);
                }
                return tree;
            });
        }
        private static DeptTallyTree[] _GetChildren ( DeptBaseDto parent, DeptBaseDto[] depts, Dictionary<long, int> empNum, Dictionary<long, string> empName )
        {
            return depts.Convert(a => a.ParentId == parent.Id, a =>
            {
                DeptTallyTree tree = new DeptTallyTree
                {
                    Id = a.Id,
                    Name = a.ShortName.GetValueOrDefault(a.DeptName),
                    IsUnit = a.IsUnit,
                    LeaderId = a.LeaderId,
                    DeptTag = a.DeptTag.ToSplit(),
                    DeptShow = a.DeptShow,
                    Children = _GetChildren(a, depts, empNum, empName)
                };
                if ( a.LeaderId.HasValue )
                {
                    tree.LeaderName = empName.GetValueOrDefault(a.LeaderId.Value);
                }
                if ( !a.IsUnit )
                {
                    tree.EmpNum = empNum.GetValueOrDefault(a.Id);
                }
                else if ( tree.Children.Length > 0 )
                {
                    tree.EmpNum = tree.Children.Sum(c => c.EmpNum);
                }
                return tree;
            });
        }
        private static DeptTree[] _GetChildren ( DeptBase parent, DeptBase[] depts )
        {
            return depts.Convert(a => a.ParentId == parent.Id, a => new DeptTree
            {
                DeptId = a.Id,
                DeptName = a.ShortName.GetValueOrDefault(a.DeptName),
                LeaderId = a.LeaderId,
                IsUnit = a.IsUnit,
                UnitId = a.UnitId,
                Children = _GetChildren(a, depts)
            });
        }
        public static UnitTree[] ToUnitTree ( this DeptBase[] depts, long parentId )
        {
            return depts.Convert(a => a.ParentId == parentId, a => new UnitTree
            {
                DeptId = a.Id,
                DeptName = a.ShortName.GetValueOrDefault(a.DeptName),
                LeaderId = a.LeaderId,
                Children = _GetUnitChildren(a, depts)
            });
        }
        private static UnitTree[] _GetUnitChildren ( DeptBase parent, DeptBase[] depts )
        {
            return depts.Convert(a => a.ParentId == parent.Id, a => new UnitTree
            {
                DeptId = a.Id,
                DeptName = a.ShortName.GetValueOrDefault(a.DeptName),
                LeaderId = a.LeaderId,
                Children = _GetUnitChildren(a, depts)
            });
        }
    }
}
