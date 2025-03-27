using Basic.HrRemoteModel;

namespace Basic.HrModel.Dept
{
    public class DeptBaseDto
    {
        public long Id
        {
            get;
            set;
        }
        public long CompanyId
        {
            get;
            set;
        }
        public long ParentId
        {
            get;
            set;
        }

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
        public HrDeptStatus Status { get; set; }
        public int Lvl { get; set; }

        /// <summary>
        /// 是否为单位
        /// </summary>
        public bool IsUnit { get; set; }

        /// <summary>
        /// 单位ID
        /// </summary>
        public long UnitId
        {
            get;
            set;
        }

        public long? LeaderId
        {
            get;
            set;
        }

    }
}
