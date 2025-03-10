﻿using Basic.HrRemoteModel.Company;
using Basic.HrRemoteModel.Company.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client.Interface;

namespace Basic.HrService.RpcEvent
{
    internal class CompanyEvent : IRpcApiService
    {
        private readonly ICompanyService _Service;

        public CompanyEvent (ICompanyService service)
        {
            this._Service = service;
        }

        public long AddCompany (AddCompany add)
        {
            return this._Service.Add(add.Datum);
        }
        public void DeleteCompany (DeleteCompany obj)
        {
            this._Service.Delete(obj.Id);
        }
        public CompanyDto GetCompany (GetCompany obj)
        {
            return this._Service.Get(obj.Id);
        }
        public CompanyDto[] GetCompanyList (GetCompanyList obj)
        {
            return this._Service.Gets(obj.ParentId, obj.IsAllChildren);
        }
        public CompanyTree[] GetCompanyTree (GetCompanyTree obj)
        {
            return this._Service.GetTrees(obj.ParentId, obj.Status);
        }

        public bool SetCompany (SetCompany obj)
        {
            return this._Service.Set(obj.Id, obj.Datum);
        }
    }
}
