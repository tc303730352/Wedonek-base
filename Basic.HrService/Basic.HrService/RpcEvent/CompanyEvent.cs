using Basic.HrRemoteModel.Company;
using Basic.HrRemoteModel.Company.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class CompanyEvent : IRpcApiService
    {
        private readonly ICompanyService _Service;

        public CompanyEvent ( ICompanyService service )
        {
            this._Service = service;
        }
        public bool SetCompanyAdminId ( SetCompanyAdminId obj )
        {
            return this._Service.SetAdminId(obj.Id, obj.AdminId);
        }
        public bool SetCompanyStatus ( SetCompanyStatus obj )
        {
            return this._Service.SetStatus(obj.Id, obj.Status);
        }
        public long AddCompany ( AddCompany add )
        {
            return this._Service.Add(add.Datum);
        }
        public void DeleteCompany ( DeleteCompany obj )
        {
            this._Service.Delete(obj.Id);
        }
        public CompanyDto GetCompany ( GetCompany obj )
        {
            return this._Service.Get(obj.Id);
        }
        public string GetCompanyName ( GetCompanyName obj )
        {
            return this._Service.GetName(obj.Id);
        }
        public CompanyDto[] GetCompanyList ( GetCompanyList obj )
        {
            return this._Service.Gets(obj.ParentId, obj.IsAllChildren);
        }
        public CompanyTree[] GetCompanyTree ()
        {
            return this._Service.GetTrees();
        }
        public CompanyTreeItem[] GetCompanyTreeItems ( GetCompanyTreeItems obj )
        {
            return this._Service.GetTreeItems(obj.ParentId);
        }
        public string[] GetCompanyNameList ( GetCompanyNameList obj )
        {
            return this._Service.GetNameList(obj.Ids);
        }
        public bool SetCompanyLeaverId ( SetCompanyLeaverId obj )
        {
            return this._Service.SetLeaverId(obj.Id, obj.LeaverId);
        }
        public bool SetCompany ( SetCompany obj )
        {
            return this._Service.Set(obj.Id, obj.Datum);
        }
    }
}
