using Basic.HrModel.Company;
using Basic.HrModel.DB;
using Basic.HrRemoteModel;
using Basic.HrRemoteModel.Company.Model;
using WeDonekRpc.Helper;
using WeDonekRpc.Helper.IdGenerator;
using WeDonekRpc.SqlSugar;

namespace Basic.HrDAL.Repository
{
    internal class CompanyDAL : BasicDAL<DBCompany, long>, ICompanyDAL
    {
        public CompanyDAL ( IRepository<DBCompany> basicDAL ) : base(basicDAL)
        {
        }
        public long Add ( DBCompany company )
        {
            company.Id = IdentityHelper.CreateId();
            company.AddTime = DateTime.Now;
            this._BasicDAL.Insert(company);
            return company.Id;
        }
        public Result[] Gets<Result> ( ComGetParam param ) where Result : class, new()
        {
            return this._BasicDAL.Gets<Result>(param.ToWhere(this._BasicDAL));
        }
        public bool CheckFullName ( string name )
        {
            return this._BasicDAL.IsExist(a => a.FullName == name);
        }
        public bool CheckShortName ( string name )
        {
            return this._BasicDAL.IsExist(a => a.ShortName == name);
        }
        public void SetStatus ( long id, HrCompanyStatus status )
        {
            if ( !this._BasicDAL.Update(a => a.Status == status, a => a.Id == id) )
            {
                throw new ErrorException("hr.company.set.fail");
            }
        }

        public void SetLeaverId ( long id, long? leaverId )
        {
            if ( !this._BasicDAL.Update(a => a.LeaverId == leaverId, a => a.Id == id) )
            {
                throw new ErrorException("hr.company.set.fail");
            }
        }
        public string GetName ( long id )
        {
            var data = this._BasicDAL.Get(a => a.Id == id, a => new
            {
                a.ShortName,
                a.FullName
            });
            if ( data == null )
            {
                return null;
            }
            return data.ShortName.IsNull() ? data.FullName : data.ShortName;
        }
        public Dictionary<long, string> GetNames ( long[] ids )
        {
            return this._BasicDAL.Gets(a => ids.Contains(a.Id), a => new
            {
                a.Id,
                a.ShortName,
                a.FullName
            }).ToDictionary(a => a.Id, a => a.ShortName.IsNull() ? a.FullName : a.ShortName);
        }

        public bool Set ( DBCompany source, CompSetArg arg, KeyValuePair<long, string>[] subs )
        {
            ISqlQueue<DBCompany> queue = this._BasicDAL.BeginQueue();
            _ = queue.Update(source, arg);
            queue.Update(subs.ConvertAll(c => new DBCompany
            {
                Id = c.Key,
                LevelCode = c.Value,
            }), "LevelCode");
            return queue.Submit() > 0;
        }

        public void SetStatus ( long[] ids, HrCompanyStatus status )
        {
            if ( !this._BasicDAL.Update(a => a.Status == status, a => ids.Contains(a.Id)) )
            {
                throw new ErrorException("hr.company.set.fail");
            }
        }
    }
}
