using Basic.HrRemoteModel;
using SqlSugar;

namespace Basic.HrModel.DB
{
    [SqlSugar.SugarTable("Dept")]
    public class DBDept
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id
        {
            get;
            set;
        }

        public long ParentId
        {
            get;
            set;
        }

        public long CompanyId
        {
            get;
            set;
        }

        /// <summary>
        /// 层级码
        /// </summary>
        public string LevelCode { get; set; }

        /// <summary>
        /// 部门名
        /// </summary>
        public string DeptName
        {
            get;
            set;
        }
        /// <summary>
        /// 部门短名
        /// </summary>
        public string ShortName
        {
            get;
            set;
        }

        /// <summary>
        /// 部门标签
        /// </summary>
        public string DeptTag { get; set; }

        /// <summary>
        /// 部门说明
        /// </summary>
        public string DeptShow
        {
            get;
            set;
        }
        /// <summary>
        /// 负责人
        /// </summary>
        public long? LeaderId
        {
            get;
            set;
        }
        /// <summary>
        /// 状态
        /// </summary>
        public HrDeptStatus Status { get; set; }

        /// <summary>
        /// 排序位
        /// </summary>
        public int Sort
        {
            get;
            set;
        }
        /// <summary>
        /// 层级
        /// </summary>
        public int Lvl
        {
            get;
            set;
        }

        /// <summary>
        /// 是否是独立单位
        /// </summary>
        public bool IsUnit
        {
            get;
            set;
        }
        /// <summary>
        /// 单位ID
        /// </summary>
        public long UnitId
        {
            get;
            set;
        }
        /// <summary>
        /// 是否作废
        /// </summary>
        public bool IsToVoid
        {
            get;
            set;
        }
    }
}
