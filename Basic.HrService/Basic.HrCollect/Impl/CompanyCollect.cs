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
            arg.LevelCode = source.LevelCode;
            HrCompanyType type = HrCompanyType.总公司;
            if ( set.ParentId == 0 )
            {
                arg.LevelCode = "|root|";
            }
            else
            {
                var prt = this._Company.Get(arg.ParentId, a => new
                {
                    a.CompanyType,
                    a.LevelCode
                });
                type = prt.CompanyType;
                arg.LevelCode = prt.LevelCode + set.ParentId + "|";
            }
            if ( set.CompanyType != source.CompanyType )
            {
                if ( set.CompanyType == HrCompanyType.子公司 && type == HrCompanyType.分公司 )
                {
                    //分公司不能开子公司
                    throw new ErrorException("hr.company.parent.is.branch");
                }
                else if ( set.CompanyType == HrCompanyType.分公司 && type == HrCompanyType.分公司 )
                {
                    //分公司不能开分公司
                    throw new ErrorException("hr.company.parent.is.branch");
                }
            }
            string code = source.LevelCode + source.Id + "|";
            var list = this._Company.Gets(a => a.LevelCode.StartsWith(code), a => new
            {
                a.LevelCode,
                a.Id
            });
            if ( list.IsNull() )
            {
                return this._Company.Update(source, arg);
            }
            string nCode = arg.LevelCode + source.Id + "|";
            return this._Company.Set(source, arg, list.ConvertAll(a => new KeyValuePair<long, string>(a.Id, a.LevelCode.Replace(code, nCode))));
        }
        private bool _Set ( DBCompany source, CompanySet set )
        {
            if ( set.CompanyType != source.CompanyType )
            {
                HrCompanyType type = HrCompanyType.总公司;
                if ( source.ParentId != 0 )
                {
                    type = this._Company.Get(a => a.Id == source.ParentId, a => a.CompanyType);
                }
                if ( set.CompanyType == HrCompanyType.子公司 && type == HrCompanyType.分公司 )
                {
                    //分公司不能开子公司
                    throw new ErrorException("hr.company.parent.is.branch");
                }
                else if ( set.CompanyType == HrCompanyType.分公司 && type == HrCompanyType.分公司 )
                {
                    //分公司不能开分公司
                    throw new ErrorException("hr.company.parent.is.branch");
                }
            }
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
            if ( ids.IsNull() )
            {
                this._Company.Delete(ids);
            }
            else
            {
                this._Company.Delete(source.Id);
            }
        }
        public bool SetLeaverId ( DBCompany source, long? levelId )
        {
            if ( source.LeaverId == levelId )
            {
                return false;
            }
            this._Company.SetLeaverId(source.Id, levelId);
            return true;
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
