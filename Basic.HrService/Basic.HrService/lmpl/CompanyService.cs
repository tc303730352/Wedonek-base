﻿using Basic.HrCollect;
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
        public CompanyTree[] GetTrees ( long? parentId, HrCompanyStatus[] status )
        {
            DBCompany[] list = this._Company.Gets<DBCompany>(new ComGetParam
            {
                Status = status,
                ParentId = parentId,
                IsAllChildren = true
            });
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

        public bool Set ( long id, CompanySet set )
        {
            DBCompany source = this._Company.Get(id);
            return this._Company.Set(source, set);
        }
    }
}
