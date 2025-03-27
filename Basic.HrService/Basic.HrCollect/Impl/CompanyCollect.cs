using System.Linq.Expressions;
using Basic.HrDAL;
using Basic.HrModel.Company;
using Basic.HrModel.DB;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Company.Model;
using WeDonekRpc.Client;
using WeDonekRpc.Helper;

namespace Basic.HrCollect.Impl
{
    internal class CompanyCollect : ICompanyCollect
    {
        private readonly ICompanyDAL _Company;

        public CompanyCollect ( ICompanyDAL company )
        {
            this._Company = company;
        }
        public bool SetStatus ( DBCompany source, HrCompanyStatus status )
        {
            if ( source.Status == status )
            {
                return false;
            }
            else if ( status == HrCompanyStatus.启用 )
            {
                long[] ids = source.LevelCode.Split('|').Where(a => a != string.Empty && a != "root").Select(long.Parse).ToArray();
                if ( ids.IsNull() )
                {
                    this._Company.SetStatus(source.Id, status);
                    return true;
                }
                ids = this._Company.Gets(c => ids.Contains(c.Id) && c.Status == HrCompanyStatus.停用, c => c.Id);
                ids = ids.Add(source.Id);
                this._Company.SetStatus(ids, status);
                return true;
            }
            else if ( status == HrCompanyStatus.停用 )
            {
                string code = source.LevelCode + source.Id + "|";
                long[] ids = this._Company.Gets(c => c.LevelCode.StartsWith(code) && c.Status == HrCompanyStatus.启用, c => c.Id);
                if ( ids.IsNull() )
                {
                    this._Company.SetStatus(source.Id, status);
                    return true;
                }
                ids = ids.Add(source.Id);
                this._Company.SetStatus(ids, status);
                return true;
            }
            else
            {
                throw new ErrorException("hr.company.status.error");
            }
        }
        public DBCompany Add ( CompanyAdd add )
        {
            string levelCode;
            if ( add.CompanyType != HrCompanyType.总公司 )
            {
                string code = this._Company.Get(add.ParentId, a => a.LevelCode);
                levelCode = code + add.ParentId + "|";
            }
            else
            {
                levelCode = string.Empty;
            }
            if ( this._Company.CheckFullName(add.FullName) )
            {
                throw new ErrorException("hr.company.name.repeat");
            }
            else if ( !add.ShortName.IsNull() && this._Company.CheckShortName(add.ShortName) )
            {
                throw new ErrorException("hr.company.short.name.repeat");
            }
            DBCompany com = add.ConvertMap<CompanyAdd, DBCompany>();
            com.LevelCode = levelCode;
            com.Status = HrCompanyStatus.起草;
            com.Id = this._Company.Add(com);
            return com;
        }
        public bool Set ( DBCompany source, CompanySet set )
        {
            if ( set.FullName != source.FullName && this._Company.CheckFullName(set.FullName) )
            {
                throw new ErrorException("hr.company.name.repeat");
            }
            else if ( !set.ShortName.IsNull() && set.ShortName != source.ShortName && this._Company.CheckShortName(set.ShortName) )
            {
                throw new ErrorException("hr.company.short.name.repeat");
            }
            if ( set.ParentId == source.ParentId )
            {
                return this._Set(source, set);
            }
            CompSetArg arg = set.ConvertMap<CompanySet, CompSetArg>();
            if ( set.ParentId == 0 )
            {
                arg.LevelCode = string.Empty;
            }
            else
            {
                string prtCode = this._Company.Get(arg.ParentId, a => a.LevelCode);
                if ( prtCode == string.Empty )
                {
                    arg.LevelCode = "|" + set.ParentId + "|";
                }
                else
                {
                    arg.LevelCode = prtCode + set.ParentId + "|";
                }
            }
            string code = ( source.LevelCode == string.Empty ? "|" : source.LevelCode ) + source.Id + "|";
            var list = this._Company.Gets(a => a.LevelCode.StartsWith(code), a => new
            {
                a.LevelCode,
                a.Id
            });
            if ( list.IsNull() )
            {
                return this._Company.Update(source, arg);
            }
            string nCode = ( arg.LevelCode == string.Empty ? "|" : arg.LevelCode ) + source.Id + "|";
            return this._Company.Set(source, arg, list.ConvertAll(a => new KeyValuePair<long, string>(a.Id, a.LevelCode.Replace(code, nCode))));
        }
        private bool _Set ( DBCompany source, CompanySet set )
        {
            return this._Company.Update(source, set);
        }
        public void Delete ( DBCompany source )
        {
            if ( source.Status != HrCompanyStatus.起草 )
            {
                throw new ErrorException("hr.company.status.error");
            }
            string code = source.LevelCode + source.Id + "|";
            long[] ids = this._Company.Gets(a => a.LevelCode.StartsWith(code), a => a.Id);
            if ( !ids.IsNull() )
            {
                this._Company.Delete(ids.Add(source.Id));
            }
            else
            {
                this._Company.Delete(source.Id);
            }
        }
        public bool SetLeaverId ( DBCompany source, long? empId )
        {
            if ( source.LeaverId == empId )
            {
                return false;
            }
            this._Company.SetLeaverId(source.Id, empId);
            source.LeaverId = empId;
            return true;
        }
        public bool SetAdminId ( DBCompany source, long? empId )
        {
            if ( source.AdminId == empId )
            {
                return false;
            }
            this._Company.SetAdminId(source.Id, empId);
            source.AdminId = empId;
            return true;
        }
        public DBCompany Get ( long id )
        {
            return this._Company.Get(id);
        }
        public Result Get<Result> ( long id ) where Result : class
        {
            return this._Company.Get<Result>(id);
        }
        public Result Get<Result> ( long id, Expression<Func<DBCompany, Result>> selector )
        {
            return this._Company.Get(id, selector);
        }
        public T[] Gets<T> ( ComGetParam param ) where T : class, new()
        {
            return this._Company.Gets<T>(param);
        }
        public T[] GetAll<T> () where T : class, new()
        {
            return this._Company.GetAll<T>();
        }
        public Dictionary<long, string> GetNames ( long[] ids )
        {
            return this._Company.GetNames(ids);
        }

        public string GetName ( long companyId )
        {
            return this._Company.GetName(companyId);
        }
        public long[] GetSubIds ( long companyId )
        {
            string code = this._Company.Get(a => a.Id == companyId, a => a.LevelCode);
            if ( code == string.Empty )
            {
                return this._Company.Gets<long>(a => a.Status == HrCompanyStatus.启用, a => a.Id);
            }
            code = code + companyId + "|";
            return this._Company.Gets<long>(a => a.LevelCode.StartsWith(code) && a.Status == HrCompanyStatus.启用, a => a.Id);
        }
        public CompanyName[] GetSubs ( string levelCode )
        {
            return this._Company.Gets<CompanyName>(a => a.LevelCode.StartsWith(levelCode) && a.Status == HrCompanyStatus.启用);
        }
        public Result[] Gets<Result> ( long[] ids ) where Result : class
        {
            return this._Company.Gets<Result>(ids);
        }

        public string[] GetNameList ( long[] ids )
        {
            var list = this._Company.Gets(ids, a => new
            {
                a.FullName,
                a.ShortName
            });
            return list.ConvertAll(c => c.FullName.GetValueOrDefault(c.FullName));
        }
    }
}
