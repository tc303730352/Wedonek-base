using Basic.HrDAL;
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

        public long Add ( CompanyAdd add )
        {
            string levelCode;
            if ( add.CompanyType != HrCompanyType.总公司 )
            {
                var prt = this._Company.Get(add.ParentId, a => new
                {
                    a.CompanyType,
                    a.LevelCode
                });
                if ( add.CompanyType == HrCompanyType.子公司 && prt.CompanyType == HrCompanyType.分公司 )
                {
                    //分公司不能开子公司
                    throw new ErrorException("hr.company.parent.is.branch");
                }
                else if ( add.CompanyType == HrCompanyType.分公司 && prt.CompanyType == HrCompanyType.分公司 )
                {
                    //分公司不能开分公司
                    throw new ErrorException("hr.company.parent.is.branch");
                }
                levelCode = prt.LevelCode + add.ParentId + "|";
            }
            else
            {
                levelCode = "|root|";
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
            return this._Company.Add(com);
        }
        public bool Set ( DBCompany source, CompanySet set )
        {
            if ( set.CompanyType != source.CompanyType )
            {
                HrCompanyType? type = this._Company.Get<HrCompanyType?>(source.ParentId, a => a.CompanyType);
                if ( set.CompanyType == HrCompanyType.子公司 && type.Value == HrCompanyType.分公司 )
                {
                    //分公司不能开子公司
                    throw new ErrorException("hr.company.parent.is.branch");
                }
                else if ( set.CompanyType == HrCompanyType.分公司 && type.Value == HrCompanyType.分公司 )
                {
                    //分公司不能开分公司
                    throw new ErrorException("hr.company.parent.is.branch");
                }
            }
            if ( set.FullName != source.FullName && this._Company.CheckFullName(set.FullName) )
            {
                throw new ErrorException("hr.company.name.repeat");
            }
            else if ( !set.ShortName.IsNull() && set.ShortName != source.ShortName && this._Company.CheckShortName(set.ShortName) )
            {
                throw new ErrorException("hr.company.short.name.repeat");
            }
            return this._Company.Update(source, set);
        }
        public void Delete ( DBCompany source )
        {
            if ( source.Status == HrCompanyStatus.启用 )
            {
                throw new ErrorException("hr.company.status.error");
            }
            this._Company.Delete(source.Id);
        }
        public DBCompany Get ( long id )
        {
            return this._Company.Get(id);
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
    }
}
