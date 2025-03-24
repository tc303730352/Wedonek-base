using Basic.HrGatewaryModular.Interface;
using Basic.HrGatewaryModular.Model.Company;
using Basic.HrRemoteModel.Company.Model;
using WeDonekRpc.ApiGateway.Attr;
using WeDonekRpc.Helper.Validate;
using WeDonekRpc.HttpApiGateway;
using WeDonekRpc.HttpApiGateway.Model;

namespace Basic.HrGatewaryModular.Api
{
    internal class CompanyApi : ApiController
    {
        private readonly ICompanyService _Service;
        public CompanyApi ( ICompanyService service )
        {
            this._Service = service;
        }
        public bool SetAdminId ( LongParam<long?> param )
        {
            return this._Service.SetAdminId(param.Id, param.Value);
        }
        /// <summary>
        /// 获取树形选择项
        /// </summary>
        /// <returns></returns>
        public CompanyTreeItem[] GetTreeItems ()
        {
            return this._Service.GetCompanyTreeItems();
        }
        /// <summary>
        /// 设置负责人
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool SetLeaverId ( LongParam<long?> param )
        {
            return this._Service.SetLeaverId(param.Id, param.Value);
        }
        /// <summary>
        /// 新增公司
        /// </summary>
        /// <param name="datum">公司资料</param>
        /// <returns></returns>
        [ApiPower("all", "hr.company.add")]
        public long Add ( [NullValidate("hr.company.datum.null")] CompanyAdd datum )
        {
            return this._Service.AddCompany(datum);
        }
        [ApiPower("all", "hr.company.set")]
        public bool Enable ( [NumValidate("hr.company.id.error", 1)] long id )
        {
            return this._Service.Enable(id);
        }
        [ApiPower("all", "hr.company.set")]
        public bool Stop ( [NumValidate("hr.company.id.error", 1)] long id )
        {
            return this._Service.Stop(id);
        }
        /// <summary>
        /// 获取树形字典项名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetName ( [NumValidate("hr.company.id.error", 1)] long id )
        {
            return this._Service.GetName(id);
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
        /// <returns></returns>
        public CompanyTree[] GetTree ()
        {
            return this._Service.GetCompanyTree();
        }

        /// <summary>
        /// 修改公司资料
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public bool Set ( [NullValidate("hr.company.param.null")] LongParam<CompanySet> param )
        {
            return this._Service.SetCompany(param.Id, param.Value);
        }

    }
}
