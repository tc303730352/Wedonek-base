using Basic.HrRemoteModel.Dept.Model;
using WeDonekRpc.Helper.Validate;
namespace Basic.HrGatewaryModular.Model.Dept
{
    /// <summary>
    /// 修改部门资料 UI参数实体
    /// </summary>
    internal class UI_SetDept
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        [NumValidate("hr.dept.id.error", 1)]
        public long Id { get; set; }

        /// <summary>
        /// 部门资料
        /// </summary>
        [NullValidate("hr.dept.datum.null")]
        public DeptSet Dept { get; set; }

    }
}
