using Basic.HrCollect;
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
        public CompanyService ( ICompanyCollect company, IEmpCollect emp )
        {
            this._Company = company;
            this._Emp = emp;
        }
        public string GetName ( long id )
        {
            return this._Company.GetName(id);
        }

        public long Add ( CompanyAdd add )
        {
            return this._Company.Add(add);
        }

        public void Delete ( long id )
        {
            DBCompany source = this._Company.Get(id);
            this._Company.Delete(source);
        }

        public CompanyDto Get ( long id )
        {
            DBCompany source = this._Company.Get(id);
            CompanyDto dto = source.ConvertMap<DBCompany, CompanyDto>();
            if ( dto.LeaverId.HasValue )
            {
                dto.Leaver = this._Emp.GetName(dto.LeaverId.Value);
            }
            return dto;
        }
        public CompanyDto[] Gets ( long? parentId, bool isAllChildren )
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
                if ( c.LeaverId.HasValue )
                {
                    c.Leaver = empName.GetValueOrDefault(c.LeaverId.Value);
                }
            });
            return list;
        }
        public CompanyTree[] GetTrees ()
        {
            DBCompany[] list = this._Company.GetAll<DBCompany>();
            if ( list.IsNull() )
            {
                return Array.Empty<CompanyTree>();
            }
            Dictionary<long, string> names = this._Emp.GetName(list.Convert(a => a.LeaverId.HasValue, a => a.LeaverId.Value));
            return list.ConvertTree<DBCompany, CompanyTree>(a => a.ParentId == 0, ( a, b ) =>
            {
                if ( a.LeaverId.HasValue )
                {
                    b.Leaver = names.GetValueOrDefault(a.LeaverId.Value);
                }
            }, ( a, b ) => a.ParentId == b.Id);
        }
        public bool SetLeaverId ( long id, long? levelId )
        {
            DBCompany source = this._Company.Get(id);
            return this._Company.SetLeaverId(source, levelId);
        }

        public bool Set ( long id, CompanySet set )
        {
            DBCompany source = this._Company.Get(id);
            return this._Company.Set(source, set);
        }
        public bool SetStatus ( long id, HrCompanyStatus status )
        {
            DBCompany source = this._Company.Get(id);
            return this._Company.SetStatus(source, status);
        }
    }
}
