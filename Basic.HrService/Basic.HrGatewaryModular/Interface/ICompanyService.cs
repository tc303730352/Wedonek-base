using Basic.HrRemoteModel.Company.Model;

namespace Basic.HrGatewaryModular.Interface
{
    public interface ICompanyService
    {
        /// <summary>
        /// 新增公司
        /// </summary>
        /// <param name="datum">公司资料</param>
        /// <returns></returns>
        long AddCompany ( CompanyAdd datum );

        /// <summary>
        /// 删除公司
        /// </summary>
        /// <param name="id">公司ID</param>
        void DeleteCompany ( long id );
        bool Enable ( long id );

        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <param name="id">公司ID</param>
        /// <returns></returns>
        CompanyDto GetCompany ( long id );

        /// <summary>
        /// 获取公司列表
        /// </summary>
        /// <param name="parentId">父级公司ID</param>
        /// <param name="isAllChildren">是否包含所有下级</param>
        /// <returns></returns>
        CompanyDto[] GetCompanyList ( long? parentId, bool isAllChildren );

        /// <summary>
        /// 获取公司列表树形结构
        /// </summary>
        /// <returns></returns>
        CompanyTree[] GetCompanyTree ();
        string GetName ( long id );

        /// <summary>
        /// 修改公司资料
        /// </summary>
        /// <param name="id">公司ID</param>
        /// <param name="datum">公司资料</param>
        /// <returns></returns>
        bool SetCompany ( long id, CompanySet datum );
        bool SetLeaverId ( long id, long? value );
        bool Stop ( long id );
    }
}
