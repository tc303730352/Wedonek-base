using Basic.HrCollect;
using Basic.HrLocalEvent.Model;
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
        public CompanyService ( ICompanyCollect company, IEmpCollect emp )
        {
            this._Company = company;
            this._Emp = emp;
        }
        public string GetName ( long id )
        {
            return this._Company.GetName(id);
        }
        public string[] GetNameList ( long[] ids )
        {
            return this._Company.GetNameList(ids);
        }
        public CompanyTreeItem[] GetTreeItems ( long? parentId )
        {
            BasicCompany[] items = this._Company.Gets<BasicCompany>(new ComGetParam
            {
                ParentId = parentId,
                IsAllChildren = true,
                Status = new HrCompanyStatus[]
                {
                    HrCompanyStatus.启用
                }
            });
            if ( !parentId.HasValue && items.IsNull() )
            {
                return Array.Empty<CompanyTreeItem>();
            }
            else if ( parentId.HasValue )
            {
                BasicCompany com = this._Company.Get<BasicCompany>(parentId.Value);
                CompanyTreeItem item = new CompanyTreeItem
                {
                    CompanyType = com.CompanyType,
                    Id = com.Id,
                    LeaverId = com.LeaverId,
                    Name = com.ShortName.GetValueOrDefault(com.FullName)
                };
                if ( !items.IsNull() )
                {
                    item.Children = items.ConvertTree<BasicCompany, CompanyTreeItem>(a => a.ParentId == com.Id, ( a, b ) =>
                    {
                        b.Name = a.ShortName.GetValueOrDefault(a.FullName);
                    }, ( a, b ) => a.ParentId == b.Id);
                }
                return new CompanyTreeItem[]
                   {
                       item
                   };
            }
            else
            {
                long pid = parentId.GetValueOrDefault(0);
                return items.ConvertTree<BasicCompany, CompanyTreeItem>(a => a.ParentId == 0, ( a, b ) =>
                {
                    b.Name = a.ShortName.GetValueOrDefault(a.FullName);
                }, ( a, b ) => a.ParentId == b.Id);
            }
        }
        public long Add ( CompanyAdd add )
        {
            DBCompany com = this._Company.Add(add);
            new CompanyEvent(com).AsyncSend("Add");
            return com.Id;
        }

        public void Delete ( long id )
        {
            DBCompany source = this._Company.Get(id);
            if ( !this._Emp.CheckIsNull(id) )
            {
                throw new ErrorException("hr.company.not.null");
            }
            this._Company.Delete(source);
            new CompanyEvent(source).AsyncSend("Delete");
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
            List<long> empId = new List<long>();
            list.ForEach(c =>
            {
                if ( c.LeaverId.HasValue )
                {
                    empId.Add(c.LeaverId.Value);
                }
                if ( c.AdminId.HasValue )
                {
                    empId.Add(c.AdminId.Value);
                }
            });
            Dictionary<long, string> names = this._Emp.GetName(empId.Distinct().ToArray());
            return list.ConvertTree<DBCompany, CompanyTree>(a => a.ParentId == 0, ( a, b ) =>
            {
                if ( a.LeaverId.HasValue )
                {
                    b.Leaver = names.GetValueOrDefault(a.LeaverId.Value);
                }
                if ( a.AdminId.HasValue )
                {
                    b.Admin = names.GetValueOrDefault(a.AdminId.Value);
                }
            }, ( a, b ) => a.ParentId == b.Id);
        }
        public bool SetAdminId ( long id, long? empId )
        {
            DBCompany source = this._Company.Get(id);
            long? old = source.AdminId;
            if ( this._Company.SetAdminId(source, empId) )
            {
                new CompanyEvent(source, old).AsyncSend("SetAdmin");
                return true;
            }
            return false;
        }
        public bool SetLeaverId ( long id, long? empId )
        {
            DBCompany source = this._Company.Get(id);
            return this._Company.SetLeaverId(source, empId);
        }

        public bool Set ( long id, CompanySet set )
        {
            DBCompany source = this._Company.Get(id);
            if ( this._Company.Set(source, set) )
            {
                new CompanyEvent(source).AsyncSend("Update");
                return true;
            }
            return false;
        }
        public bool SetStatus ( long id, HrCompanyStatus status )
        {
            DBCompany source = this._Company.Get(id);
            return this._Company.SetStatus(source, status);
        }
    }
}
