using WeDonekRpc.Helper.Validate;

namespace Basic.HrRemoteModel.Dept.Model
{
    public class DeptSet
    {

        /// <summary>
        /// 部门名
        /// </summary>
        [NullValidate("hr.dept.name.null")]
        [LenValidate("hr.dept.name.len", 2, 100)]
        public string DeptName
        {
            get;
            set;
        }
        /// <summary>
        /// 部门短名
        /// </summary>
        [LenValidate("hr.dept.short.name.len", 0, 50)]
        public string ShortName
        {
            get;
            set;
        }
        /// <summary>
        /// 父级部门ID
        /// </summary>
        public long ParentId { get; set; }
        /// <summary>
        /// 部门标签
        /// </summary>
        public string[] DeptTag { get; set; }

        /// <summary>
        /// 部门说明
        /// </summary>
        public string DeptShow
        {
            get;
            set;
        }
        /// <summary>
        /// 部门负责人
        /// </summary>
        public long? LeaderId
        {
            get;
            set;
        }
    }
}
