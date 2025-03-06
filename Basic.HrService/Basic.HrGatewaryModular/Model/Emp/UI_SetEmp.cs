using Basic.HrRemoteModel.Emp.Model;
using WeDonekRpc.Helper.Validate;
namespace Basic.HrGatewaryModular.Model.Emp
{
    /// <summary>
    /// 修改人员资料 UI参数实体
    /// </summary>
    internal class UI_SetEmp
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        [NumValidate("hr.emp.id.error", 1)]
        public long Id { get; set; }

        /// <summary>
        /// 人员资料
        /// </summary>
        [NullValidate("hr.emp.datum.null")]
        public EmpSet Datum { get; set; }

    }
}
