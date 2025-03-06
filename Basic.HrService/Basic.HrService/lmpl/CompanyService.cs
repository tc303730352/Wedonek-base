using System.Collections.Frozen;
using Basic.HrCollect;
using Basic.HrModel.Company;
using Basic.HrModel.DB;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Company.Model;
using Basic.HrService.Interface;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;
namespace Basic.HrService.lmpl
{
    internal class CompanyService : ICompanyService
    {
        private readonly ICompanyCollect _Company;
        private readonly IEmpCollect _Emp;
        public CompanyService (ICompanyCollect company, IEmpCollect emp)
        {
            this._Company = company;
            this._Emp = emp;
        }

        public long Add (CompanyAdd add)
        {
            return this._Company.Add(add);
        }

        public void Delete (long id)
        {
            DBCompany source = this._Company.Get(id);
            this._Company.Delete(source);
        }

        public CompanyDto Get (long id)
        {
            DBCompany source = this._Company.Get(id);
            CompanyDto dto = source.ConvertMap<DBCompany, CompanyDto>();
            if (dto.LeaverId.HasValue)
            {
                dto.Leaver = this._Emp.GetName(dto.LeaverId.Value);
            }
            return dto;
        }
        public CompanyDto[] Gets (long? parentId, bool isAllChildren)
        {
            DBCompany[] source = this._Company.Gets<DBCompany>(new ComGetParam
            {
                ParentId = parentId,
                IsAllChildren = isAllChildren
            });
            CompanyDto[] list = source.ConvertMap<DBCompany, CompanyDto>();
            Dictionary<long, string> empName = this._Emp.GetName(list.Convert(a => a.LeaverId.HasValue, a => a.LeaverId.Value));
            list.ForEach(c =>
            {
                if (c.LeaverId.HasValue)
                {
                    c.Leaver = empName.GetValueOrDefault(c.LeaverId.Value);
                }
            });
            return list;
        }
        public CompanyTree[] GetTrees (long? parentId, HrCompanyStatus[] status)
        {
            BasicCompany[] list = this._Company.Gets<BasicCompany>(new ComGetParam
            {
                Status = status,
                ParentId = parentId,
                IsAllChildren = true
            });
            return this._GetChildren(parentId.GetValueOrDefault(), list);
        }
        private CompanyTree[] _GetChildren (long parentId, BasicCompany[] list)
        {
            return list.Convert<BasicCompany, CompanyTree>(a => a.ParentId == parentId, a => new CompanyTree
            {
                Id = a.Id,
                CompanyType = a.CompanyType,
                LeaverId = a.LeaverId,
                Name = a.ShortName.GetValueOrDefault(a.FullName),
                Children = this._GetChildren(a.Id, list)
            });
        }
        public bool Set (long id, CompanySet set)
        {
            DBCompany source = this._Company.Get(id);
            return this._Company.Set(source, set);
        }
    }
}
