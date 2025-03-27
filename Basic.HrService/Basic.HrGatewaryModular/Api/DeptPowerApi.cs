using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.DeptPower;
using WeDonekRpc.ApiGateway.Attr;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;

namespace Basic.HrGatewaryModular.Api
{
    internal class DeptPowerApi : ApiController
    {
        private readonly IDeptPowerService _Service;
        public DeptPowerApi ( IDeptPowerService service )
        {
            this._Service = service;
        }
        /// <summary>
        /// 获取员工部门数据权限
        /// </summary>
        /// <param name="empId">参数</param>
        /// <returns></returns>
        public long[] Get ( [NumValidate("hr.emp.id.error", 1)] long empId, [NumValidate("hr.company.id.error", 1)] long companyId )
        {
            return this._Service.GetDeptPower(empId, companyId);
        }

        /// <summary>
        /// 设置员工部门权限
        /// </summary>
        /// <param name="param">参数</param>
        [ApiPower("all", "hr.emp.dept.power")]
        public void Set ( [NullValidate("hr.dept.power.param.null")] UI_SetDeptPower param )
        {
            this._Service.SetDeptPower(param.EmpId, param.CompanyId, param.DeptId);
        }

    }
}
