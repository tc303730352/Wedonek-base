using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.Company;
using Basic.HrRemoteModel.Company.Model;
using WeDonekRpc.ApiGateway.Attr;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;

namespace Basic.HrGatewaryModular.Api
{
    internal class CompanyApi : ApiController
    {
        private readonly ICompanyService _Service;
        public CompanyApi ( ICompanyService service )
        {
            this._Service = service;
        }
        /// <summary>
        /// 新增公司
        /// </summary>
        /// <param name="datum">公司资料</param>
        /// <returns></returns>
        [ApiPrower("all", "hr.company.add")]
        public long Add ( [NullValidate("hr.company.datum.null")] CompanyAdd datum )
        {
            return this._Service.AddCompany(datum);
        }

        /// <summary>
        /// 删除公司
        /// </summary>
        /// <param name="id">公司ID</param>
        public void Delete ( [NumValidate("hr.company.id.error", 1)] long id )
        {
            this._Service.DeleteCompany(id);
        }

        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <param name="id">公司ID</param>
        /// <returns></returns>
        public CompanyDto Get ( [NumValidate("hr.company.id.error", 1)] long id )
        {
            return this._Service.GetCompany(id);
        }

        /// <summary>
        /// 获取公司列表
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public CompanyDto[] GetList ( [NullValidate("hr.company.param.null")] UI_GetCompanyList param )
        {
            return this._Service.GetCompanyList(param.ParentId, param.IsAllChildren);
        }

        /// <summary>
        /// 获取公司列表树形结构
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public CompanyTree[] GetTree ( [NullValidate("hr.company.param.null")] UI_GetCompanyTree param )
        {
            return this._Service.GetCompanyTree(param.ParentId, param.Status);
        }

        /// <summary>
        /// 修改公司资料
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public bool Set ( [NullValidate("hr.company.param.null")] UI_SetCompany param )
        {
            return this._Service.SetCompany(param.Id, param.Datum);
        }

    }
}
