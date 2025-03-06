using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.EmpRole;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;

namespace Basic.HrGatewaryModular.Api
{
    internal class EmpRoleApi : ApiController
    {
        private readonly IEmpRoleService _Service;
        public EmpRoleApi (IEmpRoleService service)
        {
            this._Service = service;
        }
        /// <summary>
        /// 获取人员角色列表
        /// </summary>
        /// <param name="empId">参数</param>
        /// <returns></returns>
        public long[] Get ([NumValidate("hr.emp.id.null", 1)] long empId)
        {
            return this._Service.GetEmpRole(empId);
        }

        /// <summary>
        /// 修改人员角色
        /// </summary>
        /// <param name="param">参数</param>
        public void Set ([NullValidate("hr.emprole.param.null")] UI_SetEmpRole param)
        {
            this._Service.SetEmpRole(param.EmpId, param.RoleId);
        }

    }
}
