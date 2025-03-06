using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.DeptPrower;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;

namespace Basic.HrGatewaryModular.Api
{
    internal class DeptProwerApi : ApiController
    {
        private readonly IDeptProwerService _Service;
        public DeptProwerApi (IDeptProwerService service)
        {
            this._Service = service;
        }
        /// <summary>
        /// 获取员工部门数据权限
        /// </summary>
        /// <param name="empId">参数</param>
        /// <returns></returns>
        public long[] Get ([NumValidate("hr.emp.id.error", 1)] long empId, [NumValidate("hr.company.id.error", 1)] long companyId)
        {
            return this._Service.GetDeptPrower(empId, companyId);
        }

        /// <summary>
        /// 设置员工部门权限
        /// </summary>
        /// <param name="param">参数</param>
        public void Set ([NullValidate("hr.deptprower.param.null")] UI_SetDeptPrower param)
        {
            this._Service.SetDeptPrower(param.EmpId, param.CompanyId, param.DeptId);
        }

    }
}
