using Basic.HrGatewaryModular.Interface;
using Basic.HrRemoteModel.EmpTitle.Model;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;

namespace Basic.HrGatewaryModular.Api
{
    internal class EmpTitleApi : ApiController
    {
        private readonly IEmpTitleService _Service;
        public EmpTitleApi ( IEmpTitleService service )
        {
            this._Service = service;
        }
        /// <summary>
        /// 添加人员职务
        /// </summary>
        /// <param name="datum">职务资料</param>
        /// <returns></returns>
        public long Add ( [NullValidate("hr.emptitle.datum.null")] EmpTitleAdd datum )
        {
            return this._Service.AddEmpTitle(datum);
        }

        /// <summary>
        /// 删除人员职务
        /// </summary>
        /// <param name="id">职务ID</param>
        public void Delete ( [NumValidate("hr.emptitle.id.error", 1)] long id )
        {
            this._Service.DeleteEmpTitle(id);
        }

        /// <summary>
        /// 获取职务
        /// </summary>
        /// <param name="id">职务ID</param>
        /// <returns></returns>
        public EmpTitleData Get ( [NumValidate("hr.emptitle.id.error", 1)] long id )
        {
            return this._Service.GetEmpTitle(id);
        }

        /// <summary>
        /// 获取职务列表
        /// </summary>
        /// <param name="empId">人员ID</param>
        /// <returns></returns>
        public EmpTitleDatum[] GetList ( [NumValidate("hr.emp.id.error", 1)] long empId, [NumValidate("hr.company.id.error", 1)] long? companyId )
        {
            return this._Service.GetEmpTitleList(empId, companyId);
        }

    }
}
